using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class SpawnObjectsTest {

    private ShelfData testShelf;
    private CloneObjects testClone;
    
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

        testClone = new CloneObjects();
        testClone.FillBackShelf = true;
        testClone.FillLeftShelf = true;
        testClone.FillRightShelf = true;
    }

    [TearDown]
    public void cleanup()
    {
    }

    [Test]
    public void findObjectTest()
    {
        Assert.True(CloneObjects.SpawnObject("mntdewPrefab", new Vector3(0f,0f,0f), new Quaternion(0f,0f,0f,0f)));
    }

    [Test]
    public void findObjectFailingTest()
    {
        Assert.False(CloneObjects.SpawnObject("notExistingObject", new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f)));
    }

    [Test]
    public void FillLayerTest()
    {
        Assert.True(CloneObjects.FillLayer(testShelf, 1));
    }

    [Test]
    public void FillShelfTest()
    {
        Assert.True(CloneObjects.FillShelf(testShelf));
    }

    [Test]
    public void FillShelfTestAllShelves()
    {
        Assert.True(CloneObjects.FillShelf(testShelf));
    }

    [Test]
    public void FillNoLayerShelfTest()
    {
        testShelf.setShelves(0);
        Assert.True(CloneObjects.FillShelf(testShelf));
    }

    [Test]
    public void FillInvalidItemShelf()
    {
        testShelf.setObjectType("notExistingObject");
        Assert.False(CloneObjects.FillShelf(testShelf));
    }

    [Test]
    public void FillNoWidthShelfTest()
    {
        testShelf.setWidth(0);
        Assert.True(CloneObjects.FillShelf(testShelf));
    }

    [Test]
    public void NullShelfTest()
    {
        Assert.False(CloneObjects.FillShelf(null));
    }

    [Test]
    public void BooleansTrueTest()
    {
        Assert.True(testClone.FillBackShelf);
        Assert.True(testClone.FillLeftShelf);
        Assert.True(testClone.FillRightShelf);
        testClone.Start();
        Assert.True(testClone.backshelfDone);
        Assert.True(testClone.backshelfDone);
        Assert.True(testClone.backshelfDone);
    }
}
