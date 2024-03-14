using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAll : MonoBehaviour
{
    public QuakeTowerBehaviour quakeTower;
    [SerializeField] private float damage;
    [SerializeField] private float abilityCooldown;
    private float nextUse;

    public void TremorEffect()
    {
        if (nextUse < Time.time)
        {
            nextUse = abilityCooldown + Time.time;
            foreach (GameObject enemy in quakeTower.enemyList)
            {
                EnemyBehaviour enemyScript = enemy.GetComponent<EnemyBehaviour>();
                enemyScript.DealDamage(damage);
            }
        }
    }
}
