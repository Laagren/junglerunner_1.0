using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tile_T_data : MonoBehaviour
{
    public enum Direction
    {
        East, South, West
    }
    public Vector2 tileSize = new Vector2(20f, 20f);
    public Direction entry;
    public Direction leftExit;
    public Direction rightExit;
}

