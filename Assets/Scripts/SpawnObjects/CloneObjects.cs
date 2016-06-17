using System.Collections;
using UnityEngine;

/// <summary>
/// A script that places different groceries on the shelves in the supermarket environment.
/// Done so far: left shelf, back shelf, right shelf.
/// </summary>
public class CloneObjects : MonoBehaviour
{
    /// <summary>
    /// The start position of the items that have to be placed against the back wall.
    /// </summary>
    public static Vector3 StartPosBackwall = new Vector3(-6.344999f, 1.61f, -6.2452f);

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

    public bool BackShelfDone = false;
    public bool LeftShelfDone = false;
    public bool RightShelfDone = false;

    /// <summary>
    /// The distance that has to be left between objects on the back shelf.
    /// </summary>
    private static Vector3 distanceBetweenBack = new Vector3(0f, 0f, 0.65f);

    /// <summary>
    /// The height distance between objects on the back shelf.
    /// </summary>
    private static Vector3 heightDistanceBack = new Vector3(0f, -0.44f, 0f);

    /// <summary>
    /// The rotation of the objects on the back shelf.
    /// </summary>
    private static Quaternion rotationBack = Quaternion.identity;

    /// <summary>
    /// The amount of items per layer on the back shelf.
    /// </summary>
    private static int amountBack = 20;

    /// <summary>
    /// The type of objects to spawn on the back shelf.
    /// </summary>
    private static string backObjectType = "mntdewPrefab";

    /// <summary>
    /// The start position of the items that have to be placed against the left wall.
    /// </summary>
    private static Vector3 startPosLeftwall = new Vector3(6.5062f, 1.61f, -6.0052f);

    /// <summary>
    /// The distance that has to be left between objects on the left shelf.
    /// </summary>
    private static Vector3 distanceBetweenLeft = new Vector3(-0.32f, 0f, 0f);

    /// <summary>
    /// The height distance between objects on the left shelf.
    /// </summary>
    private static Vector3 heightDistanceLeft = new Vector3(0f, -0.44f, 0f);

    /// <summary>
    /// The rotation of the objects on the left shelf.
    /// </summary>
    private static Quaternion rotationLeft = Quaternion.Euler(new Vector3(0, 180, -90));

    /// <summary>
    /// The amount of items per layer on the left shelf.
    /// </summary>
    private static int amountLeft = 40;

    /// <summary>
    /// The type of objects to spawn on the left shelf.
    /// </summary>
    private static string objectTypeLeft = "doritosPrefab";

    /// <summary>
    /// The start position of the items that have to be placed against the right wall.
    /// </summary>
    private static Vector3 startPosRightwall = new Vector3(-5.9258f, 1.61f, 6.2051f);

    /// <summary>
    /// The distance that has to be left between objects on the right shelf.
    /// </summary>
    private static Vector3 distanceBetweenRight = new Vector3(0.30f, 0f, 0f);

    /// <summary>
    /// The height distance between objects on the right shelf.
    /// </summary>
    private static Vector3 heightDistanceRight = new Vector3(0f, -0.44f, 0f);

    /// <summary>
    /// The rotation of the objects on the right shelf.
    /// </summary>
    private static Quaternion rotationRight = Quaternion.Euler(new Vector3(-90, 270, 0));

    /// <summary>
    /// The amount of items per layer on the right shelf.
    /// </summary>
    private static int amountRight = 46;

    /// <summary>
    /// The type of objects to spawn on the right shelf.
    /// </summary>
    private static string objectTypeRight = "ricePrefab";

    /// <summary>
    /// An objects holding the data of the back shelf.
    /// </summary>
    private static ShelfData backShelf = new ShelfData(backObjectType, StartPosBackwall, distanceBetweenBack, heightDistanceBack, rotationBack, amountBack);

    /// <summary>
    /// An objects holding the data of the left shelf.
    /// </summary>
    private static ShelfData leftShelf = new ShelfData(objectTypeLeft, startPosLeftwall, distanceBetweenLeft, heightDistanceLeft, rotationLeft, amountLeft);

    /// <summary>
    /// An objects holding the data of the left shelf.
    /// </summary>
    private static ShelfData rightShelf = new ShelfData(objectTypeRight, startPosRightwall, distanceBetweenRight, heightDistanceRight, rotationRight, amountRight);

    /// <summary>
    /// Fills a shelf.
    /// </summary>
    /// <param name="shelf">The ShelfData object holding the data for the shelf.</param>
    /// <returns>A boolean, telling if the actions succeeded or not.</returns>
    public static bool FillShelf(ShelfData shelf)
    {
        if (shelf == null)
        {
            return false;
        }

        bool result = true;
        for (int i = 0; i < shelf.GetShelves(); i++)
        {
            if (FillLayer(shelf, i) == false)
            {
                result = false;
            }
        }

        return result;
    }

    /// <summary>
    /// Fills one layer of a shelf.
    /// </summary>
    /// <param name="shelf">The shelf on which this layer has to be filled.</param>
    /// <param name="layer">The layer.</param>
    /// <returns>A boolean, telling if the actions succeeded or not.</returns>
    public static bool FillLayer(ShelfData shelf, int layer)
    {
        Vector3 position = shelf.GetSTartPos();
        position += layer * shelf.GetHeightDistance();
        for (int i = 0; i < shelf.GetWidth(); i++)
        {
            if (SpawnObject(shelf.GetObjectType(), position, shelf.GetRotation()) == true)
            {
                position += shelf.GetDistanceBetween();
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Spawns an object
    /// </summary>
    /// <param name="o">The string representing the object.</param>
    /// <param name="pos">The position to spawn the object on.</param>
    /// <param name="quat">The rotation to spawn the object in.</param>
    /// <returns>A boolean, telling if the actions succeeded or not.</returns>
    public static bool SpawnObject(string o, Vector3 pos, Quaternion quat)
    {
        GameObject obj = GameObject.Find(o);
        if (obj != null)
        {
            MonoBehaviour.Instantiate(obj, pos, quat);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Starts this instance.
    /// </summary>
    public void Start()
    {
        if (this.FillBackShelf)
        {
            this.BackShelfDone = FillShelf(backShelf);
        }

        if (this.FillLeftShelf)
        {
            this.LeftShelfDone = FillShelf(leftShelf);
        }

        if (this.FillRightShelf)
        {
            this.RightShelfDone = FillShelf(rightShelf);
        }

        CloneObjects.print("Spawning objects...");
        CloneObjects.print("Back shelf: " + this.BackShelfDone);
        CloneObjects.print("Left shelf: " + this.LeftShelfDone);
        CloneObjects.print("Right shelf: " + this.RightShelfDone);
    }

    /// <summary>
    /// Updates this instance.
    /// </summary>
    public void Update()
    {
    }
}