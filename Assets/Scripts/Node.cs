using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public bool walkable;
    public Vector3 worldPosition;
    public int gCost, hCost;
    public int gridX, gridY;
    public Node parent;

    public Node(bool _walkable, Vector3 _worldPos, int grid_X, int grid_Y)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = grid_X;
        gridY = grid_Y;


    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}
