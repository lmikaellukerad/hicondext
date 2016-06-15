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
        Assert.AreEqual(testShelf.getObjectType(), "mntdewPrefab");
	}

    [Test]
    public void getStartPosTest()
    {
        Assert.AreEqual(testShelf.getStartPos(), new Vector3(-2.198f, 0.6f, -1.9f));
    }

    [Test]
    public void getDistanceBetweenTest()
    {
        Assert.AreEqual(testShelf.getDistanceBetween(), new Vector3(0f, 0f, 0.25f));
    }

    [Test]
    public void getHeightDistanceTest()
    {
        Assert.AreEqual(testShelf.getHeightDistance(), new Vector3(0f, -0.13f, 0f));
    }

    [Test]
    public void getRotationTest()
    {
        Assert.AreEqual(testShelf.getRotation(), Quaternion.identity);
    }

    [Test]
    public void getShelvesTest()
    {
        Assert.AreEqual(testShelf.getShelves(), 4);
    }

    [Test]
    public void getWidthTest()
    {
        Assert.AreEqual(testShelf.getWidth(), 17);
    }

    [Test]
    public void setObjectTypeTest()
    {
        testShelf.setObjectType("Boodschap");
        Assert.AreEqual(testShelf.getObjectType(), "Boodschap");
    }

    [Test]
    public void setStartPosTest()
    {
        testShelf.setStartPos(new Vector3(1f, 2f, 3f));
        Assert.AreEqual(testShelf.getStartPos(), new Vector3(1f, 2f, 3f));
    }

    [Test]
    public void setDistanceBetweenTest()
    {
        testShelf.setDistanceBetween(new Vector3(3f, 2f, 1f));
        Assert.AreEqual(testShelf.getDistanceBetween(), new Vector3(3f, 2f, 1f));
    }

    [Test]
    public void setHeightDistanceTest()
    {
        testShelf.setHeightDistance(new Vector3(5f, 6f, 7f));
        Assert.AreEqual(testShelf.getHeightDistance(), new Vector3(5f, 6f, 7f));
    }

    [Test]
    public void setRotationTest()
    {
        testShelf.setRotation(Quaternion.Euler(new Vector3(2f, 4f, 6f)));
        Assert.AreEqual(testShelf.getRotation(), Quaternion.Euler(new Vector3(2f, 4f, 6f)));
    }

    [Test]
    public void setShelvesTest()
    {
        testShelf.setShelves(100);
        Assert.AreEqual(testShelf.getShelves(), 100);
    }

    [Test]
    public void setWidthTest()
    {
        testShelf.setWidth(1);
        Assert.AreEqual(testShelf.getWidth(), 1);
    }
}
