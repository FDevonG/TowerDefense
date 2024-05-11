using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    public Vector2Int StartCoordinates
    {
        get { return startCoordinates; }
    }
    [SerializeField] Vector2Int destinationCoordinates;
    public Vector2Int DestinationCoordinates
    {
        get { return destinationCoordinates; }
    }

    Node startNode;
    Node destinationNode;
    Node currentSearchNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if(gridManager != null)
        {
            grid = gridManager.Grid;
            startNode = grid[startCoordinates];
            destinationNode = grid[destinationCoordinates];
        }
    }

    void Start()
    {
        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(startCoordinates);
    }

    public List<Node> GetNewPath(Vector2Int currentCoordinates)
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(currentCoordinates);
        return BuildPath();
    }

    /// <summary>
    /// Explores the nodes connected to the current node in the breadthfirstsearch function, adding them to the que the algorithm is cycling through 
    /// </summary>
    private void ExploreNeighbors()
    { 
        List<Node> neighbors = new List<Node>();
        
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoordinates = currentSearchNode.coordinates + direction;
            
            if(grid.ContainsKey(neighborCoordinates))
            {                
                neighbors.Add(grid[neighborCoordinates]);
            }
        }
        foreach(Node nodeNeighbor in neighbors)
        {
            if(!reached.ContainsKey(nodeNeighbor.coordinates) && nodeNeighbor.isWalkable)
            {
                nodeNeighbor.connectedTo = currentSearchNode;
                nodeNeighbor.isConnected = true;
                reached.Add(nodeNeighbor.coordinates, nodeNeighbor);
                frontier.Enqueue(nodeNeighbor);
            }
        }
    }

    /// <summary>
    /// Controls the path finding, starting at the start node and then calls the eExploreNeighbors(); to get the connectd nodes
    /// </summary>
    void BreadthFirstSearch(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;

        frontier.Clear();
        reached.Clear();
        
        frontier.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);

        while(frontier.Count > 0)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();
            if(currentSearchNode.coordinates == destinationCoordinates) break;
        }
    }

    /// <summary>
    /// Looks back through the nodes using the connctedTo variable creating a chain of nodes. when it has goen through all the coinnected nodes it reverses the order and returns them
    /// </summary>
    /// <returns></returns>
    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while(currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            currentNode.isPath = true;
            path.Add(currentNode);
        }
        path.Reverse();
        return path;
    }


    /// <summary>
    /// Checks to see if we can place a tower without blockinbg off th path entirely. Changes the state of the isWalkable bool on the tile to false and tries to find a new path
    /// </summary>
    /// <param name="coordinates"></param>
    /// <returns></returns>
    public bool WillBlockPath(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;
            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            
            grid[coordinates].isWalkable = previousState;
            if(newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Broadcasts the message to use th RecalculatePath function if the object has one
    /// </summary>
    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }
}
