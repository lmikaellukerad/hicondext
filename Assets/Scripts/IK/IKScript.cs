using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// IK script used to solve the inverse kinematics of an arm.
/// </summary>
public class IKScript : MonoBehaviour
{
    public Transform ChainStart;
    public Transform ChainEnd;
    public GameObject Goal;
    public Transform Pole;
    public bool ConnectPole;
    private Transform chainRoot;
    private List<Vector3> polePositions = new List<Vector3>();
    private List<Quaternion> childRotations = new List<Quaternion>();
    private Quaternion chainEndRotateCorrection;

    public bool ChainFound { get; private set; }

    public List<IKJoint> Chain { get; private set; }

    /// <summary>
    /// Builds the chain and remembers starting rotations of the chain.
    /// </summary>
    public void Start()
    {
        this.Chain = new List<IKJoint>();
        this.chainRoot = (new GameObject("IKChain")).transform;
        this.chainRoot.position = this.ChainStart.position;
        this.PointChainRoot();
        this.chainRoot.parent = this.ChainStart.parent;
        this.ChainStart.parent = this.chainRoot;
        this.ChainFound = this.BuildChain(this.ChainStart);
        if (this.ConnectPole)
        {
            this.Pole.parent = this.chainRoot;
        }

        this.chainEndRotateCorrection = this.ChainEnd.rotation;
    }

    /// <summary>
    /// Updates the chain if the chain was found.
    /// </summary>
    public void Update()
    {
        if (this.ChainFound && this.Goal.activeSelf)
        {
            this.ResetChildRotations();
            this.SolveIK();
            this.ConstrainJoints();
            this.ChainEnd.rotation = this.Goal.transform.rotation * this.chainEndRotateCorrection;
        }
    }

    /// <summary>
    /// Determine the signed angle between two vectors, with normal 'n'
    /// as the rotation axis.
    /// </summary>
    /// <param name="v1">The first vector to compare.</param>
    /// <param name="v2">The second vector to compare.</param>
    /// <param name="n">The normal to determine if the angle is negative or positive.</param>
    /// <returns>The signed angle between the two vectors using the normal vector.</returns>
    public float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
    {
        return Mathf.Atan2(
            Vector3.Dot(n, Vector3.Cross(v1, v2)),
            Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
    }

    /// <summary>
    /// Draw all local poles and show lines to the joints that violates the constraint.
    /// </summary>
    public void OnDrawGizmos()
    {
        if (this.ChainFound && this.Goal)
        {
            this.polePositions.Clear();
            this.UpdatePolePositions(0);
            for (int i = 0; i < this.polePositions.Count; i++)
            {
                Gizmos.DrawSphere(this.polePositions[i], 0.02f);
            }

            for (int i = 0; i < this.Chain.Count - 1; i++)
            {
                IKJoint j1 = this.Chain[i];
                IKJoint j2 = this.Chain[i + 1];
                Vector3 targetPos = (j1.Joint.forward * j1.JointLength) + j1.Joint.position;
                Vector3 target = targetPos - j2.Joint.position;
                Vector3 otherJoint = j1.Joint.position - j2.Joint.position;
                Vector3 poleVector = this.polePositions[i + 1] - j2.Joint.position;

                if (Vector3.Angle(otherJoint, poleVector) > Vector3.Angle(target, poleVector))
                {
                    Gizmos.DrawLine(j1.Joint.position, this.polePositions[i]);
                    Gizmos.DrawLine(j2.Joint.position, this.polePositions[i]);
                }
            }
        }
    }

    /// <summary>
    /// Points the chain root his forward in the direction of the goal and the up vector to the pole.
    /// </summary>
    private void PointChainRoot()
    {
        Vector3 look = this.Goal.transform.position - this.chainRoot.position;
        Vector3 poleVector = this.Pole.position - this.chainRoot.position;
        Vector3.Normalize(look);
        Vector3 orth = poleVector - Vector3.Project(poleVector, look);
        this.chainRoot.rotation = Quaternion.LookRotation(look, orth);
    }

    /// <summary>
    /// Resets the child rotations for all joints.
    /// </summary>
    private void ResetChildRotations()
    {
        for (int i = 0; i < this.Chain.Count; i++)
        {
            this.Chain[i].Joint.GetChild(0).transform.localRotation = this.childRotations[i];
        }
    }

    /// <summary>
    /// Recursive function used to build the chain.
    /// </summary>
    /// <param name="node">The starting node.</param>
    /// <returns>Boolean to indicate if the chain end was found.</returns>
    private bool BuildChain(Transform node)
    {
        if (node.Equals(this.ChainEnd))
        {
            return true;
        }

        for (int i = 0; i < node.childCount; i++)
        {
            Transform child = node.GetChild(i);
            if (this.BuildChain(child))
            {
                IKJoint j = new IKJoint(node, child, this.Pole);
                if (this.Chain.Count != 0)
                {
                    this.Chain[0].Joint.parent = node;
                }

                this.Chain.Add(j);
                this.childRotations.Add(node.localRotation);
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Recursive function that updates the local pole position for each joint.
    /// </summary>
    /// <param name="i">The current index.</param>
    /// <returns>The Vector3 of the next pole position in the recursion.</returns>
    private Vector3 UpdatePolePositions(int i)
    {
        if (i == this.Chain.Count - 1)
        {
            this.polePositions.Insert(0, this.Pole.position);
            return this.Pole.position;
        }
        else
        {
            Quaternion rotDif = Quaternion.FromToRotation(this.Chain[i + 1].Joint.forward, this.Chain[i].Joint.forward);
            Vector3 newPole = (rotDif * (this.UpdatePolePositions(i + 1) - this.Chain[i + 1].Joint.position)) + this.Chain[i].Joint.position;
            this.polePositions.Insert(0, newPole);
            return newPole;
        }
    }

    /// <summary>
    /// Constrains the joints to prevent impossible rotations.
    /// It uses the local pole for each joint.
    /// </summary>
    private void ConstrainJoints()
    {
        this.UpdatePolePositions(1);
        for (int i = 0; i < this.Chain.Count - 1; i++)
        {
            IKJoint j1 = this.Chain[i];
            IKJoint j2 = this.Chain[i + 1];
            this.Constrain(j1, j2, this.polePositions[i]);
        }
    }

    /// <summary>
    /// Constrains two joints if their angle goes against the pole.
    /// Constraining is done by inverting their local x rotation seen from the joint to the goal.
    /// </summary>
    /// <param name="j1">The first joint to constrain.</param>
    /// <param name="j2">The second joint to constrain.</param>
    /// <param name="pole">The pole to compare against.</param>
    private void Constrain(IKJoint j1, IKJoint j2, Vector3 pole)
    {
        Vector3 targetPos = (j1.Joint.forward * j1.JointLength) + j1.Joint.position;
        Vector3 target = targetPos - j2.Joint.position;
        Vector3 otherJoint = j1.Joint.position - j2.Joint.position;
        Vector3 poleVector = pole - j2.Joint.position;

        if (Vector3.Angle(otherJoint, poleVector) > Vector3.Angle(target, poleVector))
        {
            Vector3 startJoint = j2.Joint.position - j1.Joint.position;
            float angle2 = -this.AngleSigned(targetPos - j1.Joint.position, startJoint, j1.Joint.right);
            j1.Joint.Rotate(new Vector3(1, 0, 0), (180 - angle2) * 2);
            float angle = this.AngleSigned(otherJoint, target, j2.Joint.right);
            j2.Joint.Rotate(new Vector3(1, 0, 0), angle * 2);
            targetPos -= j1.Joint.position;
        }
    }

    /// <summary>
    /// Solves the inverse kinematics of the chain by iterating over the joints and updating each of them.
    /// Joints only rotate over their local x-axis and all the other rotations are done at the start in the chain root.
    /// </summary>
    private void SolveIK()
    {
        Vector3 root = this.ChainStart.position;
        Transform jointTarget = this.Goal.transform;
        this.PointChainRoot();
        for (int i = 0; i < this.Chain.Count; i++)
        {
            IKJoint j = this.Chain[i];
            this.UpdateJoint(j, jointTarget);
            if (jointTarget != this.Goal.transform)
            {
                jointTarget.parent = j.Joint.GetChild(0);
            }

            if (i == this.Chain.Count - 1)
            {
                j.Joint.position = root;
            }
            else
            {
                j.Joint.parent = null;
                jointTarget = j.Joint;
            }
        }
    }

    /// <summary>
    /// Rotates the joint on its local x-axis to point to its target and then sets the new position of the joint to reach the target.
    /// </summary>
    /// <param name="joint">The joint.</param>
    /// <param name="target">The target.</param>
    private void UpdateJoint(IKJoint joint, Transform target)
    {
        Vector3 next = target.position - joint.Joint.position;
        Vector3 current = joint.Joint.forward * joint.JointLength;
        float angle = -this.AngleSigned(next, current, joint.Joint.right);
        joint.Joint.Rotate(new Vector3(1, 0, 0), angle);
        joint.Joint.position = (-joint.Joint.forward * joint.JointLength) + target.position;
    }
}
