using System.Collections;
using UnityEngine;

/// <summary>
/// Clones the Doritos object into the scene at given locations.
/// </summary>
public class CloneDoritos : MonoBehaviour
{
    /// <summary>
    /// The start position of the items that have to be placed against the back wall
    /// </summary>
    private Vector3 startPosLeftwall;

    /// <summary>
    /// The distance that has to be left between objects on the X axis
    /// </summary>
    private Vector3 distanceBetweenX;

    /// <summary>
    /// The height distance between two layers on the shelves
    /// </summary>
    private Vector3 heightDistance;

    /// <summary>
    /// The current position to spawn objects on
    /// </summary>
    private Vector3 position;

    /// <summary>
    /// The current rotation to spawn objects in
    /// </summary>
    private Quaternion rotation;

    /// <summary>
    /// Starts this instance.
    /// </summary>
    public void Start()
    {
        this.startPosLeftwall = new Vector3(2.428f, 0.5f, -2.14f);
        this.distanceBetweenX = new Vector3(-0.12f, 0f, 0f);
        this.heightDistance = new Vector3(0f, -0.13f, 0f);
        this.rotation = Quaternion.Euler(new Vector3(0, 180, -90));

        for (int i = 0; i < 4; i++)
        {
            this.FillLeftWallShelf(i);
        }
    }

    /// <summary>
    /// Updates this instance.
    /// </summary>
    public void Update()
    {
    }

    /// <summary>
    /// Fills one layer of the shelf that is standing against the left wall with doritos.
    /// </summary>
    /// <param name="layer">The layer that has to be filled.</param>
    private void FillLeftWallShelf(int layer)
    {
        this.position = this.startPosLeftwall;
        this.position += layer * this.heightDistance;
        for (int i = 0; i < 38; i++)
        {
            this.SpawnDoritos(this.position, this.rotation);
            this.position += this.distanceBetweenX;
        }
    }

    /// <summary>
    /// Spawns a doritos object at given position and rotation.
    /// </summary>
    /// <param name="pos">The position.</param>
    /// <param name="quat">The rotation.</param>
    private void SpawnDoritos(Vector3 pos, Quaternion quat)
    {
        GameObject mntDew = GameObject.Find("doritosPrefab");
        if (mntDew != null)
        {
            MonoBehaviour.Instantiate(mntDew, pos, quat);
        }
    }
}
