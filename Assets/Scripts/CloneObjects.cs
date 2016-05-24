using System.Collections;
using UnityEngine;

/// <summary>
/// A script that places different groceries on the shelves in the supermarket environment.
/// Done so far: left shelf, back shelf, right shelf.
/// </summary>
public class CloneObjects : MonoBehaviour
{
    /// <summary>
    /// Whether or not to fill the back shelf, can be switched from within Unity.
    /// </summary>
    public bool FillBackShelf;

    /// <summary>
    /// Whether or not to fill the left shelf, can be switched from within Unity.
    /// </summary>
    public bool FillLeftShelf;

    /// <summary>
    /// Whether or not to fill the right shelf, can be switched from within Unity.
    /// </summary>
    public bool FillRightShelf;

    /// <summary>
    /// The start position of the items that have to be placed against the back wall.
    /// </summary>
    private Vector3 startPosBackwall = new Vector3(-2.198f, 0.6f, -1.875f);

    /// <summary>
    /// The distance that has to be left between objects on the Z axis.
    /// </summary>
    private Vector3 distanceBetweenZ = new Vector3(0f, 0f, 0.25f);

    /// <summary>
    /// The start position of the items that have to be placed against the left wall.
    /// </summary>
    private Vector3 startPosLeftwall = new Vector3(2.428f, 0.5f, -2.14f);

    /// <summary>
    /// The start position of the items that have to be placed against the right wall.
    /// </summary>
    private Vector3 startPosRightwall = new Vector3(-1.98f, 0.559f, 2.174f);

    /// <summary>
    /// The distance that has to be left between objects on the X axis.
    /// </summary>
    private Vector3 distanceBetweenX = new Vector3(-0.12f, 0f, 0f);

    /// <summary>
    /// The distance that has to be left between objects on the right wall.
    /// </summary>
    private Vector3 distanceBetweenR = new Vector3(0.1f, 0f, 0f);

    /// <summary>
    /// The height distance between two layers on the shelves.
    /// </summary>
    private Vector3 heightDistance;

    /// <summary>
    /// The current position to spawn objects on.
    /// </summary>
    private Vector3 position;

    /// <summary>
    /// The current rotation to spawn objects in.
    /// </summary>
    private Quaternion rotation;

    /// <summary>
    /// The amount of layers per shelf.
    /// </summary>
    private int layers = 4;

    /// <summary>
    /// The amount of items per layer on the left shelf.
    /// </summary>
    private int leftAmount = 38;

    /// <summary>
    /// The amount of items per layer on the back shelf.
    /// </summary>
    private int backAmount = 17;

    /// <summary>
    /// The amount of items per layer on the right shelf.
    /// </summary>
    private int rightAmount = 46;

    /// <summary>
    /// Starts this instance.
    /// </summary>
    public void Start()
    {
        if (this.FillBackShelf)
        {
            this.BackShelfFiller();
        }

        if (this.FillLeftShelf)
        {
            this.LeftShelfFiller();
        } 
        
        if (this.FillRightShelf)
        {
            this.RightShelfFiller();
        }
    }

    /// <summary>
    /// Updates this instance.
    /// </summary>
    public void Update()
    {
    }

    /// <summary>
    /// Fills the backmost shelf.
    /// </summary>
    public void BackShelfFiller()
    {
        this.rotation = Quaternion.identity;
        this.heightDistance = new Vector3(0f, -0.13f, 0f);
        this.position = this.startPosBackwall;

        for (int i = 0; i < this.layers; i++)
        {
            this.FillBackWallShelfLayer(i);
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

        for (int i = 0; i < this.layers; i++)
        {
            this.FillLeftWallShelfLayer(i);
            this.position = this.startPosLeftwall;
        }
    }

    /// <summary>
    /// Fills the rightmost shelf.
    /// </summary>
    public void RightShelfFiller()
    {
        this.heightDistance = new Vector3(0f, -0.13f, 0f);
        this.rotation = Quaternion.Euler(new Vector3(-90, 270, 0));
        this.position = this.startPosRightwall;

        for (int i = 0; i < this.layers; i++)
        {
            this.FillRightWallShelfLayer(i);
            this.position = this.startPosRightwall;
        }
    }

    /// <summary>
    /// Fills one layer of the shelf that is standing against the back wall with mt dew.
    /// </summary>
    /// <param name="layer">The layer that has to be filled. layers are 0, 1, 2, 3 from top to bottom.</param>
    private void FillBackWallShelfLayer(int layer)
    {
        this.position += layer * this.heightDistance;
        this.position -= this.distanceBetweenZ;
        for (int i = 0; i < this.backAmount; i++)
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
    private void FillLeftWallShelfLayer(int layer)
    {
        this.position += layer * this.heightDistance;
        for (int i = 0; i < this.leftAmount; i++)
        {
            this.SpawnDoritos(this.position, this.rotation);
            this.position += this.distanceBetweenX;
        }
    }

    /// <summary>
    /// Fills one layer of the shelf that is standing against the right wall with rice.
    /// </summary>
    /// <param name="layer">The layer that has to be filled. layers are 0, 1, 2, 3 from top to bottom.</param>
    private void FillRightWallShelfLayer(int layer)
    {
        this.position += layer * this.heightDistance;
        this.position -= this.distanceBetweenR;
        for (int i = 0; i < this.rightAmount; i++)
        {
            // print("spawning #" + i);
            this.SpawnRice(this.position, this.rotation);
            this.position += this.distanceBetweenR;
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

    /// <summary>
    /// Spawns a rice object at given position and rotation.
    /// </summary>
    /// <param name="pos">The position.</param>
    /// <param name="quat">The rotation.</param>
    private void SpawnRice(Vector3 pos, Quaternion quat)
    {
        GameObject rice = GameObject.Find("ricePrefab");
        if (rice != null)
        {
            MonoBehaviour.Instantiate(rice, pos, quat);
        }
    }
}
