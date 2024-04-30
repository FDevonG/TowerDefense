using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

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
            path.Add(child.GetComponent<Waypoint>());
        }
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
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
        GetComponent<Enemy>().StealGold();
        gameObject.SetActive(false);
    }
}
