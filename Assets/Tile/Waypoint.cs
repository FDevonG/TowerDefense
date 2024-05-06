using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable
    {
        get { return isPlaceable; }
    }

    [SerializeField] Tower towerPrefab;
    
    void OnMouseDown()
    {
        if (isPlaceable)
        {
            bool wasPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            if (wasPlaced)
                isPlaceable = false;
            else
                isPlaceable = true;
        }
    }
}
