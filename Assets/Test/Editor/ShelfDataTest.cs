using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class ShelfDataTest {

    protected ShelfData testShelf;

    [SetUp]
    public virtual void setup()
    {
        GameObject one = new GameObject("mntdewPrefab");
        Vector3 startPosTest = new Vector3(-2.198f, 0.6f, -1.9f);
        Vector3 distanceBetweenTest = new Vector3(0f, 0f, 0.25f);
        Vector3 heightDistanceTest = new Vector3(0f, -0.13f, 0f);
        Quaternion rotationTest = Quaternion.identity;
        int amountTest = 17;
        string testObjectType = "mntdewPrefab";
        testShelf = new ShelfData(testObjectType, startPosTest, distanceBetweenTest, heightDistanceTest, rotationTest, amountTest);
    }

	[Test]
    public void getObjectTypeTest()
	{
        Assert.AreEqual(testShelf.GetObjectType(), "mntdewPrefab");
	}

    [Test]
    public void getStartPosTest()
    {
        Assert.AreEqual(testShelf.GetSTartPos(), new Vector3(-2.198f, 0.6f, -1.9f));
    }

    [Test]
    public void getDistanceBetweenTest()
    {
        Assert.AreEqual(testShelf.GetDistanceBetween(), new Vector3(0f, 0f, 0.25f));
    }

    [Test]
    public void getHeightDistanceTest()
    {
        Assert.AreEqual(testShelf.GetHeightDistance(), new Vector3(0f, -0.13f, 0f));
    }

    [Test]
    public void getRotationTest()
    {
        Assert.AreEqual(testShelf.GetRotation(), Quaternion.identity);
    }

    [Test]
    public void getShelvesTest()
    {
        Assert.AreEqual(testShelf.GetShelves(), 4);
    }

    [Test]
    public void getWidthTest()
    {
        Assert.AreEqual(testShelf.GetWidth(), 17);
    }

    [Test]
    public void setObjectTypeTest()
    {
        testShelf.SetObjectType("Boodschap");
        Assert.AreEqual(testShelf.GetObjectType(), "Boodschap");
    }

    [Test]
    public void setStartPosTest()
    {
        testShelf.SetStartPos(new Vector3(1f, 2f, 3f));
        Assert.AreEqual(testShelf.GetSTartPos(), new Vector3(1f, 2f, 3f));
    }

    [Test]
    public void setDistanceBetweenTest()
    {
        testShelf.SetDistanceBetween(new Vector3(3f, 2f, 1f));
        Assert.AreEqual(testShelf.GetDistanceBetween(), new Vector3(3f, 2f, 1f));
    }

    [Test]
    public void setHeightDistanceTest()
    {
        testShelf.SetHeightDistance(new Vector3(5f, 6f, 7f));
        Assert.AreEqual(testShelf.GetHeightDistance(), new Vector3(5f, 6f, 7f));
    }

    [Test]
    public void setRotationTest()
    {
        testShelf.SetRotation(Quaternion.Euler(new Vector3(2f, 4f, 6f)));
        Assert.AreEqual(testShelf.GetRotation(), Quaternion.Euler(new Vector3(2f, 4f, 6f)));
    }

    [Test]
    public void setShelvesTest()
    {
        testShelf.SetShelves(100);
        Assert.AreEqual(testShelf.GetShelves(), 100);
    }

    [Test]
    public void setWidthTest()
    {
        testShelf.SetWidth(1);
        Assert.AreEqual(testShelf.GetWidth(), 1);
    }
}
