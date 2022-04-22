using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findParent : MonoBehaviour
{
    public enum Direction
    {
        Left, Right
    }
    public TileData parent;
    private float removeTimer;
    private bool startRemove, removeRight, removeLeft;
    public Direction placement;
    private void Awake()
    {
        parent = GetComponentInParent<TileData>(); 

    }

    void Update()
    {
        if (startRemove)
        {
            removeTimer += Time.deltaTime;
            if (removeTimer >= 1f)
            {
                if (removeLeft)
                {
                    parent.RemoveLeftSide();
                    removeLeft = false;
                }
                else if (removeRight)
                {
                    parent.RemoveRightSide();
                    removeRight = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        startRemove = true;
        if (placement == Direction.Left)
        {
            removeRight = true;
        }
        if (placement == Direction.Right)
        {
            removeLeft = true;
        }
    }
}
