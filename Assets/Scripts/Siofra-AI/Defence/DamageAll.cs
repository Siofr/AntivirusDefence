using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAll : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float abilityCooldown;
    private float nextUse;

    public void TremorEffect()
    {
        if (nextUse < Time.time)
        {
            nextUse = abilityCooldown + Time.time;
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                EnemyBehaviour enemyScript = enemy.GetComponent<EnemyBehaviour>();
                enemyScript.DealDamage(damage);
            }
        }
    }
}
