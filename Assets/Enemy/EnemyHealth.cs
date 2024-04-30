using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int defaultHealthPoints = 5;
    public int HealthPoints { get; private set; }
    
    void OnEnable()
    {
        HealthPoints = defaultHealthPoints;
    }

    public void ProcessHit()
    {
        HealthPoints--;
        if (HealthPoints <= 0)
        {
            GetComponent<Enemy>().RewardGold();
            gameObject.SetActive(false);
        }
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }
}
