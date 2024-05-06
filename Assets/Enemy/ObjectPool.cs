using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Tooltip("Instantied to attack the player")][SerializeField] GameObject batteringRam;
    [Tooltip("Used to determine how big the enemy pool size is")][SerializeField] [Range(0, 50)] int poolSize = 5;
    [Tooltip("Used to determine how often the enemies are spawned")][SerializeField] [Range(0.1f, 30f)] float spawnTimer = 1;

    GameObject[] enemyPool;

    void Awake()
    {
        BuildObjectPool();
    }
    
    void Start()
    {
        StartCoroutine(StartEnemy());
    }

    void BuildObjectPool()
    {
        Debug.Log("Building Pool");
        enemyPool = new GameObject[poolSize];
        for (int i = 0; i < enemyPool.Length; i++)
        {
           enemyPool[i] = Instantiate(batteringRam, transform);
           enemyPool[i].SetActive(false);
        }
    }

    void EnablePoolObject()
    {
        for (int i = 0; i < enemyPool.Length; i++)
        {
            if(!enemyPool[i].activeInHierarchy)
            {
                enemyPool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator StartEnemy()
    {
        while (true)
        {
            EnablePoolObject();
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
