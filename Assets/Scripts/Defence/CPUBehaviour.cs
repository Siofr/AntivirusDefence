using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUBehaviour : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;

    public void DealDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            // Game Over
        }
    }
}
