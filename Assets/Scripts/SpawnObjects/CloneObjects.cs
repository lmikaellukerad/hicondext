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
    private static Vector3 startPosBackwall = new Vector3(-2.198f, 0.6f, -1.9f);

    /// <summary>
    /// The distance that has to be left between objects on the back shelf.
    /// </summary>
    private static Vector3 distanceBetweenBack = new Vector3(0f, 0f, 0.25f);

    /// <summary>
    /// The height distance between objects on the back shelf.
    /// </summary>
    private static Vector3 heightDistanceBack = new Vector3(0f, -0.13f, 0f);

    /// <summary>
    /// The rotation of the objects on the back shelf.
    /// </summary>
    private static Quaternion rotationBack = Quaternion.identity;

    /// <summary>
    /// The amount of items per layer on the back shelf.
    /// </summary>
    private static int amountBack = 17;
    
    /// <summary>
    /// The type of objects to spawn on the back shelf.
    /// </summary>
    private static string backObjectType = "mntdewPrefab";

    /// <summary>
    /// The start position of the items that have to be placed against the left wall.
    /// </summary>
    private static Vector3 startPosLeftwall = new Vector3(2.428f, 0.5f, -2.14f);

    /// <summary>
    /// The distance that has to be left between objects on the left shelf.
    /// </summary>
    private static Vector3 distanceBetweenLeft = new Vector3(-0.12f, 0f, 0f);

    /// <summary>
    /// The height distance between objects on the left shelf.
    /// </summary>
    private static Vector3 heightDistanceLeft = new Vector3(0f, -0.13f, 0f);

    /// <summary>
    /// The rotation of the objects on the left shelf.
    /// </summary>
    private static Quaternion rotationLeft = Quaternion.Euler(new Vector3(0, 180, -90));

    /// <summary>
    /// The amount of items per layer on the left shelf.
    /// </summary>
    private static int amountLeft = 38;

    /// <summary>
    /// The type of objects to spawn on the left shelf.
    /// </summary>
    private static string objectTypeLeft = "doritosPrefab";

    /// <summary>
    /// The start position of the items that have to be placed against the right wall.
    /// </summary>
    private static Vector3 startPosRightwall = new Vector3(-2.1f, 0.559f, 2.174f);

    /// <summary>
    /// The distance that has to be left between objects on the right shelf.
    /// </summary>
    private static Vector3 distanceBetweenRight = new Vector3(0.1f, 0f, 0f);

    /// <summary>
    /// The height distance between objects on the right shelf.
    /// </summary>
    private static Vector3 heightDistanceRight = new Vector3(0f, -0.13f, 0f);

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
    private static ShelfData backShelf = new ShelfData(backObjectType, startPosBackwall, distanceBetweenBack, heightDistanceBack, rotationBack, amountBack);

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
        for (int i = 0; i < shelf.getShelves(); i++)
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
        Vector3 position = shelf.getStartPos();
        position += layer * shelf.getHeightDistance();
        for (int i = 0; i < shelf.getWidth(); i++)
        {
            if (SpawnObject(shelf.getObjectType(), position, shelf.getRotation()) == true)
            {
                position += shelf.getDistanceBetween();
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
        bool backshelfDone = false;
        bool leftshelfDone = false;
        bool rightshelfDone = false;

        if (this.FillBackShelf)
        {
            backshelfDone = FillShelf(backShelf);
        }

        if (this.FillLeftShelf)
        {
            leftshelfDone = FillShelf(leftShelf);
        }

        if (this.FillRightShelf)
        {
            rightshelfDone = FillShelf(rightShelf);
        }

        CloneObjects.print("Spawning objects...");
        CloneObjects.print("Back shelf: " + backshelfDone);
        CloneObjects.print("Left shelf: " + leftshelfDone);
        CloneObjects.print("Right shelf: " + rightshelfDone);
    }

    /// <summary>
    /// Updates this instance.
    /// </summary>
    public void Update()
    {
    }
}