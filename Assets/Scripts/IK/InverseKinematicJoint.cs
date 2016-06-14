using System.Collections;
using UnityEngine;

/// <summary>
/// Joint class containing the joints transform and joint length
/// </summary>
public class InverseKinematicJoint
{
    private float jointLength;
    private Transform joint;

    /// <summary>
    /// Initializes a new instance of the <see cref="InverseKinematicJoint"/> class.
    /// Creates a Joint object and points it to the pole.
    /// Parents a transform to this new game object.
    /// </summary>
    /// <param name="bone">The transform used to create the joint.</param>
    /// <param name="child">The child used to calculate the joint length.</param>
    /// <param name="pole">The pole to point the new joint to.</param>
    public InverseKinematicJoint(Transform bone, Transform child, Transform pole)
    {
        this.joint = (new GameObject("joint_" + bone.name)).transform;
        Vector3 look = child.position - bone.position;
        Vector3 poleVector = pole.position - bone.position;
        Vector3.Normalize(look);
        Vector3 orth = poleVector - Vector3.Project(poleVector, look);

        this.joint.rotation = Quaternion.LookRotation(look, orth);
        this.joint.position = bone.position;
        this.joint.parent = bone.parent;
        bone.parent = this.joint;
        this.jointLength = Vector3.Distance(bone.position, child.position);
    }

    /// <summary>
    /// Gets or sets the transform of the joint.
    /// </summary>
    /// <value>
    /// The transform.
    /// </value>
    public Transform Joint
    {
        get { return this.joint; }
        set { this.joint = value; }
    }

    /// <summary>
    /// Gets or sets the length of the joint.
    /// </summary>
    /// <value>
    /// The length of the joint.
    /// </value>
    public float JointLength
    {
        get { return this.jointLength; }
        set { this.jointLength = value; }
    }
}
