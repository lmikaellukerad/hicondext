using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// IK script used to solve the inverse kinematics of an arm.
/// </summary>
public class IKScript : MonoBehaviour
{
    public Transform ChainStart;
    public Transform ChainEnd;
    public Transform Goal;
    public Transform Pole;
    public bool ConnectPole;
    public bool chainFound {get; private set;}
    private Transform chainRoot;
    public List<IKJoint> chain {get; private set;}

    /// <summary>
    /// Starts this instance.
    /// </summary>
    public void Start()
    {
        chain = new List<IKJoint>();
        this.chainRoot = (new GameObject("IKChain")).transform;
        this.chainRoot.position = this.ChainStart.position;
        this.chainRoot.parent = this.ChainStart.parent;
        this.ChainStart.parent = this.chainRoot;
        this.chainFound = this.BuildChain(this.ChainStart);
        print(chainFound);
        this.SolveIK();
        if (this.ConnectPole)
        {
            this.Pole.parent = this.chainRoot;
        }
    }

    /// <summary>
    /// Updates this instance.
    /// </summary>
    public void Update()
    {
        if (this.chainFound)
        {
            this.SolveIK();
            this.Constraint();
            this.SolveIK();
        }
    }

    /// <summary>
    /// Draws a line gizmo when a joint violates the constraint.
    /// </summary>
    public void OnDrawGizmos()
    {
        for (int i = 0; i < this.chain.Count - 1; i++)
        {
            IKJoint j1 = this.chain[i];
            IKJoint j2 = this.chain[i + 1];
            Vector3 targetPos = (j1.Joint.forward * j1.JointLength) + j1.Joint.position;
            Vector3 target = targetPos - j2.Joint.position;
            Vector3 otherJoint = j1.Joint.position - j2.Joint.position;
            Vector3 poleVector = this.Pole.position - j2.Joint.position;

            if (Vector3.Angle(otherJoint, poleVector) > Vector3.Angle(target, poleVector))
            {
                Gizmos.DrawLine(otherJoint + j2.Joint.position, poleVector + j2.Joint.position);
                Gizmos.DrawLine(target + j2.Joint.position, poleVector + j2.Joint.position);
            }
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
                this.chain.Add(new IKJoint(node, child, this.Pole));
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Constrains the current chain using the given pole.
    /// </summary>
    private void Constraint()
    {
        for (int i = 0; i < this.chain.Count - 1; i++)
        {
            IKJoint j1 = this.chain[i];
            IKJoint j2 = this.chain[i + 1];
            Vector3 targetPos = (j1.Joint.forward * j1.JointLength) + j1.Joint.position;
            Vector3 target = targetPos - j2.Joint.position;
            Vector3 otherJoint = j1.Joint.position - j2.Joint.position;
            Vector3 poleVector = this.Pole.position - j2.Joint.position;

            if (Vector3.Angle(otherJoint, poleVector) > Vector3.Angle(target, poleVector))
            {
                Vector3 startJoint = j2.Joint.position - j1.Joint.position;
                float angle2 = Vector3.Angle(targetPos - j1.Joint.position, startJoint);
                j1.Joint.Rotate(j1.Joint.right, (180 - angle2) * 2);
                print(j1.Joint);
                print(j2.Joint);
                float angle = Vector3.Angle(otherJoint, target);
                j2.Joint.Rotate(j2.Joint.right, -angle * 2);
                targetPos -= j1.Joint.position;
            }
        }
    }

    /// <summary>
    /// Solves the inverse kinematics.
    /// </summary>
    /// <returns>Boolean indicating if the hand goal was reached</returns>
    private bool SolveIK()
    {
        Vector3 root = this.ChainStart.position;
        Transform jointTarget = this.Goal;
        for (int i = 0; i < this.chain.Count; i++)
        {
            IKJoint j = this.chain[i];
            this.UpdateJoint(j, jointTarget);
            if (jointTarget != this.Goal)
            {
                jointTarget.parent = j.Joint;
            }

            if (i == this.chain.Count - 1)
            {
                j.Joint.position = root;
            }
            else
            {
                j.Joint.parent = null;
                jointTarget = j.Joint;
            }
        }

        Vector3 look = this.Goal.position - this.chainRoot.position;
        Vector3.Normalize(look);
        Vector3 orth = this.Pole.position - Vector3.Project(this.Pole.position, look);

        this.chainRoot.rotation = Quaternion.LookRotation(look, orth);
        return true;
    }

    /// <summary>
    /// Updates the joint.
    /// </summary>
    /// <param name="joint">The joint.</param>
    /// <param name="target">The target.</param>
    private void UpdateJoint(IKJoint joint, Transform target)
    {
        Vector3 look = target.position - joint.Joint.position;
        Vector3 poleVector = this.Pole.position - joint.Joint.position;
        Vector3.Normalize(look);
        Vector3 orth = this.Pole.position - Vector3.Project(this.Pole.position, look);
        joint.Joint.rotation = Quaternion.LookRotation(look, orth);
        joint.Joint.position = (-joint.Joint.forward * joint.JointLength) + target.position;
    }
}
