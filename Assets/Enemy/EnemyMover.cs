using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    public List<Node> path = new List<Node>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    Pathfinder pathfinder;
    GridManager gridManager;

    Enemy enemy;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        pathfinder = FindObjectOfType<Pathfinder>();
        gridManager = FindObjectOfType<GridManager>();
    }

    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
        
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates;

        if (resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        } 
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        
        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowWaypoints());
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    IEnumerator FollowWaypoints()
    {
        for(int i = 1; i < path.Count; i++) 
        {
            Vector3 waypoint = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint;
            float progress = 0f;
            transform.LookAt(endPosition);

            while (progress < 1f)
            {
                progress += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, progress);
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }
}
