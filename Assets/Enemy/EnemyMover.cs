using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    public List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    Enemy enemy;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowWaypoints());
    }

    void FindPath()
    {
        path.Clear();

        Transform pathTiles = GameObject.Find("PathTiles").transform;
        foreach(Transform child in pathTiles)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();
            if(waypoint != null)
            {
                path.Add(child.GetComponent<Waypoint>());
            }
        }
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
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
            Waypoint waypoint = path[i];
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
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
