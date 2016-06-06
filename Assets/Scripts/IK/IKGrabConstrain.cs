using System.Collections;
using UnityEngine;

public class IKGrabConstrain : IKScript
{
    public Transform ReferencePoint;
    public bool Constrain = false;
    private bool oldConstrain = false;
    private Vector3 goalRefDifPosition;
    private Quaternion goalRefDifRotation;
    private Quaternion referenceInitRotation;

    /// <summary>
    /// Updates the chain if the chain was found.
    /// </summary>
    public void Update()
    {
        if (this.ChainFound && this.Goal.activeSelf)
        {
            this.ResetChildRotations();
            this.ConstrainGoal();
            this.SolveIK();
            this.ConstrainJoints();
            this.ChainEnd.rotation = this.Goal.transform.rotation * this.chainEndRotateCorrection;
        }
    }

    /// <summary>
    /// Calculates the differences in rotation and position between the reference point and the current goal.
    /// Also sets the initial reference rotation.
    /// </summary>
    private void CalculateDiff()
    {
        if (this.ReferencePoint != null)
        {
            this.goalRefDifPosition = this.Goal.transform.position - this.ReferencePoint.position;
            this.goalRefDifRotation = this.Goal.transform.rotation * Quaternion.Inverse(this.ReferencePoint.rotation);
            this.referenceInitRotation = this.ReferencePoint.rotation;
        }
    }

    /// <summary>
    /// Constrains the goal to the reference point to stop it from passing past the starting distance.
    /// </summary>
    private void ConstrainGoal()
    {
        if (this.Constrain != this.oldConstrain)
        {
            this.CalculateDiff();
            this.oldConstrain = this.Constrain;
        }

        if (this.Constrain && this.ReferencePoint != null)
        {
            Quaternion refRotCompensate = this.ReferencePoint.rotation * Quaternion.Inverse(this.referenceInitRotation);
            Vector3 currentPosDif = refRotCompensate * (this.Goal.transform.position - this.ReferencePoint.position);
            currentPosDif = Vector3.Project(currentPosDif, Vector3.Normalize(this.goalRefDifPosition));
            if (currentPosDif.magnitude < this.goalRefDifPosition.magnitude)
            {
                this.Goal.transform.position = (refRotCompensate * this.goalRefDifPosition) + this.ReferencePoint.position;
                this.Goal.transform.rotation = this.ReferencePoint.rotation * this.goalRefDifRotation;
            }
        }
    }
}
