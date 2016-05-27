using UnityEngine;
using System.Collections;

public class ShelfData
{
    private string objectType;
    private Vector3 startPos;
    private Vector3 distanceBetween;
    private Vector3 heightDistance;
    private Quaternion rotation;
    private int shelves;
    private int width;

    public ShelfData(string obj, Vector3 sp, Vector3 db, Vector3 hd, Quaternion rot, int w)
    {
        this.objectType = obj;
        this.startPos = sp;
        this.distanceBetween = db;
        this.heightDistance = hd;
        this.rotation = rot;
        this.shelves = 4;
        this.width = w;
    }

    public string getObjectType()
    {
        return this.objectType;
    }

    public Vector3 getStartPos()
    {
        return this.startPos;
    }

    public Vector3 getDistanceBetween()
    {
        return distanceBetween;
    }

    public Vector3 getHeightDistance()
    {
        return heightDistance;
    }

    public Quaternion getRotation()
    {
        return rotation;
    }

    public int getShelves()
    {
        return shelves;
    }

    public int getWidth()
    {
        return this.width;
    }

    public void setObjectType(string o)
    {
        this.objectType = o;
    }

    public void setStartPos(Vector3 sp)
    {
        this.startPos = sp;
    }

    public void setDistanceBetween(Vector3 db)
    {
        this.distanceBetween = db;
    }

    public void setHeightDistance(Vector3 hd)
    {
        this.heightDistance = hd;
    }

    public void setRotation(Quaternion rot)
    {
        this.rotation = rot;
    }

    public void setShelves(int s)
    {
        this.shelves = s;
    }

    public void setWidth(int w)
    {
        this.width = w;
    }
}
