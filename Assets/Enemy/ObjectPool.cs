using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Tooltip("Instantied to attack the player")][SerializeField] GameObject batteringRam;
    [Tooltip("Used to determine how big the enemy pool size is")][SerializeField] int poolSize = 5;
    [Tooltip("Used to determine how often the enemies are spawned")][SerializeField] float spawnTimer = 1;

    public List<GameObject> EnemyPool{ get; private set; }

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
        EnemyPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
           EnemyPool.Add(Instantiate(batteringRam, transform));
           EnemyPool[i].SetActive(false);
        }
    }

    void EnablePoolObject()
    {
        for (int i = 0; i < EnemyPool.Count; i++)
        {
            if(!EnemyPool[i].activeInHierarchy)
            {
                EnemyPool[i].SetActive(true);
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
