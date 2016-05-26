using System.Collections;
using UnityEngine;

/// <summary>
/// Joint class containing the joints transform and joint length
/// </summary>
public class IKJoint
{
    private float jointLength;
    private Transform joint;

    /// <summary>
    /// Initializes a new instance of the <see cref="IKJoint"/> class.
    /// Creates a Joint object and points it to the pole.
    /// Parents a transform to this new game object.
    /// </summary>
    /// <param name="trans">The transform used to create the joint.</param>
    /// <param name="child">The child used to calculate the joint length.</param>
    /// <param name="pole">The pole to point the new joint to.</param>
    public IKJoint(Transform trans, Transform child, Transform pole)
    {
        this.joint = (new GameObject("joint_" + trans.name)).transform;
        Vector3 look = child.position - trans.position;
        Vector3.Normalize(look);
        Vector3 orth = pole.position - Vector3.Project(pole.position, look);

        this.joint.rotation = Quaternion.LookRotation(look, orth);
        this.joint.position = trans.position;
        this.joint.parent = trans.parent;
        trans.parent = this.joint;
        this.jointLength = Vector3.Distance(trans.position, child.position);
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
