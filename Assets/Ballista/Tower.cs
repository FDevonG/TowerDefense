using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int costOfTower = 50;

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
}
