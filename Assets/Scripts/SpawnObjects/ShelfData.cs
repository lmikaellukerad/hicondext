using System.Collections;
using UnityEngine;

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

    public string GetObjectType()
    {
        return this.objectType;
    }

    public Vector3 GetSTartPos()
    {
        return this.startPos;
    }

    public Vector3 GetDistanceBetween()
    {
        return this.distanceBetween;
    }

    public Vector3 GetHeightDistance()
    {
        return this.heightDistance;
    }

    public Quaternion GetRotation()
    {
        return this.rotation;
    }

    public int GetShelves()
    {
        return this.shelves;
    }

    public int GetWidth()
    {
        return this.width;
    }

    public void SetObjectType(string o)
    {
        this.objectType = o;
    }

    public void SetStartPos(Vector3 sp)
    {
        this.startPos = sp;
    }

    public void SetDistanceBetween(Vector3 db)
    {
        this.distanceBetween = db;
    }

    public void SetHeightDistance(Vector3 hd)
    {
        this.heightDistance = hd;
    }

    public void SetRotation(Quaternion rot)
    {
        this.rotation = rot;
    }

    public void SetShelves(int s)
    {
        this.shelves = s;
    }

    public void SetWidth(int w)
    {
        this.width = w;
    }
}
