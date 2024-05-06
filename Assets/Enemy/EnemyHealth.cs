using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealthPoints = 5;
    [Tooltip("Used to increase the max hit points every time they die")]
    [SerializeField] int difficultyRamp = 1;

    Enemy enemy;

    public int HealthPoints { get; private set; }
    
    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        HealthPoints = maxHealthPoints;
    }

    public void ProcessHit()
    {
        HealthPoints--;
        if (HealthPoints <= 0)
        {
            enemy.RewardGold();
            maxHealthPoints += difficultyRamp;
            gameObject.SetActive(false);
        }
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }
}
