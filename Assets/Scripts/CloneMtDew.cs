using System.Collections;
using UnityEngine;

/// <summary>
/// Clones the mtDew object into the scene at given locations.
/// </summary>
public class CloneMtDew : MonoBehaviour 
{
    /// <summary>
    /// The start position of the items that have to be placed against the back wall
    /// </summary>
    private Vector3 startPosBackwall;

    /// <summary>
    /// The distance that has to be left between objects on the Z axis
    /// </summary>
    private Vector3 distanceBetweenZ;

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
        this.startPosBackwall = new Vector3(-2.198f, 0.6f, -1.875f);
        this.distanceBetweenZ = new Vector3(0f, 0f, 0.25f);
        this.heightDistance = new Vector3(0f, -0.13f, 0f);
        this.position = new Vector3(0f, 0f, 0f);

        for (int i = 0; i < 4; i++)
        {
            this.FillBackWallShelf(i);
        }
    }

    /// <summary>
    /// Updates this instance.
    /// </summary>
    public void Update() 
    {
    }

    /// <summary>
    /// Fills one layer of the shelf that is standing against the back wall with mt dew.
    /// </summary>
    /// <param name="layer">The layer that has to be filled.</param>
    private void FillBackWallShelf(int layer)
    {
        this.position = this.startPosBackwall;
        this.position += layer * this.heightDistance;
        this.position -= this.distanceBetweenZ;
        this.rotation = Quaternion.identity;
        for (int i = 0; i < 17; i++)
        {
            // print("spawning #" + i);
            this.SpawnMtDew(this.position, this.rotation);
            this.position += this.distanceBetweenZ;
        }
    }

    /// <summary>
    /// Spawns a mt Dew object at given position and rotation.
    /// </summary>
    /// <param name="pos">The position.</param>
    /// <param name="quat">The rotation.</param>
    private void SpawnMtDew(Vector3 pos, Quaternion quat)
    {
        GameObject mntDew = GameObject.Find("mntdewPrefab");
        if (mntDew != null)
            {
                // print("mntdewPrefab found, spawning...");
                MonoBehaviour.Instantiate(mntDew, pos, quat);
            }
    }
}
