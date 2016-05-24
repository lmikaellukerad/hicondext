using System.Collections;
using UnityEngine;

public class CloneObjects : MonoBehaviour 
{
    /// <summary>
    /// The start position of the items that have to be placed against the back wall
    /// </summary>
    private Vector3 startPosBackwall = new Vector3(-2.198f, 0.6f, -1.875f);

    /// <summary>
    /// The distance that has to be left between objects on the Z axis
    /// </summary>
    private Vector3 distanceBetweenZ = new Vector3(0f, 0f, 0.25f);

    /// <summary>
    /// The start position of the items that have to be placed against the left wall
    /// </summary>
    private Vector3 startPosLeftwall = new Vector3(2.428f, 0.5f, -2.14f);

    /// <summary>
    /// The distance that has to be left between objects on the X axis
    /// </summary>
    private Vector3 distanceBetweenX = new Vector3(-0.12f, 0f, 0f);

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
        this.BackShelfFiller();
        this.LeftShelfFiller();
    }

    /// <summary>
    /// Updates this instance.
    /// </summary>
    public void Update()
    {
    }

    /// <summary>
    /// Fills the rightmost shelf.
    /// </summary>
    public void BackShelfFiller()
    {
        this.heightDistance = new Vector3(0f, -0.13f, 0f);
        this.position = this.startPosBackwall;

        for (int i = 0; i < 4; i++)
        {
            this.FillBackWallShelf(i);
            this.position = this.startPosBackwall;
        }
    }

    /// <summary>
    /// Fills the leftmost shelf.
    /// </summary>
    public void LeftShelfFiller()
    {
        this.heightDistance = new Vector3(0f, -0.13f, 0f);
        this.rotation = Quaternion.Euler(new Vector3(0, 180, -90));
        this.position = this.startPosLeftwall;

        for (int i = 0; i < 4; i++)
        {
            this.FillLeftWallShelf(i);
            this.position = this.startPosLeftwall;
        }
    }

    /// <summary>
    /// Fills one layer of the shelf that is standing against the back wall with mt dew.
    /// </summary>
    /// <param name="layer">The layer that has to be filled. layers are 0, 1, 2, 3 from top to bottom.</param>
    private void FillBackWallShelf(int layer)
    {
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
    /// Fills one layer of the shelf that is standing against the left wall with doritos.
    /// </summary>
    /// <param name="layer">The layer that has to be filled.</param>
    private void FillLeftWallShelf(int layer)
    {
        this.position += layer * this.heightDistance;
        for (int i = 0; i < 38; i++)
        {
            this.SpawnDoritos(this.position, this.rotation);
            this.position += this.distanceBetweenX;
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
