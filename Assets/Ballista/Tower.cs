using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int costOfTower = 75;
    [SerializeField][Range(0.1f, 10f)] float buildDelay = 1f;

    void OnEnable()
    {
        StartCoroutine("BuildTower");
    }

    public bool CreateTower(Tower towerPrefab, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if (bank == null)
        {
            return false;
        }
        if(bank.CurrentBalance >= costOfTower)
        {
            bank.Withdraw(costOfTower);
            Instantiate(towerPrefab, position, Quaternion.identity);
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator BuildTower()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach(Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(false);
            }
        }

        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);
            foreach(Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(true);
            }
        }
    }
}
