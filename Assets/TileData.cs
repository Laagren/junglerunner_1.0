using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TileData : MonoBehaviour
{       
    public enum Direction
    {
        North, East, South, West, None
    }
    public Vector2 tileSize = new Vector2(20f, 20f);
    public Direction entry;
    public Direction exit;
    public Direction leftExit;
    public Direction rightExit;
    public string tileName;
    public GameObject []rightSideObjects;
    public GameObject[] leftSideObjects;

    private void Awake()
    {
        leftSideObjects = GameObject.FindGameObjectsWithTag("LeftSide");
        rightSideObjects = GameObject.FindGameObjectsWithTag("RightSide");
    }

    //public void Remove(GameObject[] tiles)
    //{
    //    foreach (GameObject tile in tiles)
    //    {
    //        Destroy(tile);
    //    }
    //}

    public void RemoveLeftSide()
    {
        foreach (GameObject obj in leftSideObjects)
        {
            Destroy(obj);
        }
    }

    public void RemoveRightSide()
    {
        foreach (GameObject obj in rightSideObjects)
        {
            Destroy(obj);
        }
    }
    //public void RemoveOtherSide(string tag)
    //{
    //    if (tag == "RightSide")
    //    {
    //        Remove(leftSideObjects);
    //    }
    //    else if (tag == "LeftSide")
    //    {
    //        Remove(rightSideObjects);
    //    }
    //    //foreach (GameObject trig in leftSideObjects)
    //    //{
    //    //    if (trig == trigger)
    //    //    {
    //    //        Remove(rightSideObjects);
    //    //    }
    //    //    else
    //    //    {
    //    //        Remove(leftSideObjects);
    //    //    }
    //    //}
    //}   
}
