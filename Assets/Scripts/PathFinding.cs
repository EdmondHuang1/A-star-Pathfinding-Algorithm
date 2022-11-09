using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class PathFinding : MonoBehaviour
{

    public Transform seeker, target;
    Stopwatch timer = new Stopwatch();
    Grid grid;
    public float speed;
    void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void Start()
    {
        timer.Start();

        print("started A* timer");
    }

    private void Update()
    {
        FindPath(seeker.position, target.position);
        if(Vector3.Distance(seeker.position,target.position) < 3.5)
        {
            timer.Stop();

            TimeSpan timeTaken = timer.Elapsed;

            print("A* Time taken: " + timeTaken.ToString(@"m\:ss\.fff"));
        }

    }
    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while(openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for(int i = 1; i < openSet.Count; i++)
            {
                if(openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if(currentNode == targetNode)
            {
                retracePath(startNode, targetNode);
                return;
            }

            foreach(Node neighbour in grid.GetNeighbours(currentNode))
            {
                if(!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    if (currentNode.walkable && neighbour.walkable)
                    {
                        //seeker.transform.position = Vector3.MoveTowards(seeker.transform.position, neighbour.worldPosition, (2.0f * Time.deltaTime));   // STUCK
                    }
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + getDistance(currentNode, neighbour);
                if(newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = getDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }

                    
                }
                
            }

        }

    }

    void retracePath(Node start, Node end)
    {
        List<Node> path = new List<Node>();
        Node currentNode = end;

        while(currentNode != start)
        {
            
            path.Add(currentNode);
            currentNode = currentNode.parent;
            
            seeker.transform.position = Vector3.MoveTowards(seeker.transform.position, currentNode.worldPosition, ((speed/10) * Time.deltaTime));   // MOST SUCCESS SO FAR
        }

        path.Reverse();

        grid.path = path;

    }

    int getDistance(Node a, Node b)
    {
        int distX = Mathf.Abs(a.gridX - b.gridX);
        int distY = Mathf.Abs(a.gridY - b.gridY);

        if(distX > distY)
        {
            return 14 * distY + 10 * (distX - distY);
        }
        else
        {
            return 14 * distX + 10 * (distY - distX);
        }
    }
}
