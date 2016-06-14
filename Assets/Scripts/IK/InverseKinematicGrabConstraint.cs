using System.Collections;
using UnityEngine;

public class InverseKinematicGrabConstraint : InverseKinematicBehaviour
{
    private Transform referencePoint;
    private Vector3 goalRefDifPosition;
    private Quaternion goalRefDifRotation;
    private Quaternion referenceInitRotation;
    private Quaternion goalInitRotation;

    /// <summary>
    /// Updates the chain if the chain was found.
    /// </summary>
    public override void Update()
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
    /// Sets the reference point back to null.
    /// </summary>
    public void Reset()
    {
        this.referencePoint = null;
    }

    public void ConstrainedUpdate(Transform obj)
    {
        if (obj != this.referencePoint)
        {
            this.referencePoint = obj;
            this.CalculateDiff();
        }

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
        if (this.referencePoint != null)
        {
            this.goalRefDifPosition = this.Goal.transform.position - this.referencePoint.position;
            this.goalRefDifRotation = this.Goal.transform.rotation * Quaternion.Inverse(this.referencePoint.rotation);
            this.referenceInitRotation = this.referencePoint.rotation;
            this.goalInitRotation = this.Goal.transform.rotation;
        }
    }

    /// <summary>
    /// Constrains the goal to the reference point to stop it from passing past the starting distance.
    /// </summary>
    private void ConstrainGoal()
    {
        if (this.referencePoint != null)
        {
            Quaternion refRotCompensate = this.referencePoint.rotation * Quaternion.Inverse(this.referenceInitRotation);
            Vector3 currentPosDif = refRotCompensate * (this.Goal.transform.position - this.referencePoint.position);
            currentPosDif = Vector3.Project(currentPosDif, Vector3.Normalize(this.goalRefDifPosition));
            if (currentPosDif.sqrMagnitude + currentPosDif.sqrMagnitude*0.05 < this.goalRefDifPosition.sqrMagnitude)
            {
                this.Goal.transform.position = (refRotCompensate * this.goalRefDifPosition) + this.referencePoint.position;
                this.Goal.transform.rotation = refRotCompensate * this.goalInitRotation;
            }
        }
    }
}
