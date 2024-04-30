using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] float range = 15f;
    [SerializeField] ParticleSystem projectiles;
    [SerializeField] Transform target;

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        float maxDistance = Mathf.Infinity;
        Transform closestTarget = null;
        foreach(Enemy enemy in enemies) 
        {
            if(enemy.gameObject.activeInHierarchy)
            {
                float distance = Vector3.Distance(weapon.position, enemy.transform.position);
                if(distance < maxDistance)
                {
                    maxDistance = distance;
                    closestTarget = enemy.transform;
                }
            }
        }
        target = closestTarget;
    }

    private void AimAtTarget()
    {
        if(Vector3.Distance(transform.position, target.position) > range)
        {
            Attack(false);
        }
        else 
        {
            Attack(true);
        }
        weapon.LookAt(target);
    }

    private void Attack(bool isActive)
    {
        var emission = projectiles.emission;
        emission.enabled = isActive;
    }

    void Update()
    {
        FindClosestTarget();
        AimAtTarget();
    }
}
