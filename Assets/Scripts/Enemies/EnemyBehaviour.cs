using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour, IDamageable
{
    public EnemyStats enemyStats;
    public UnityEvent playEffect;
    private NavMeshAgent agent;
    private GameObject playerBase;
    private float health;

    void Awake()
    {
        health = enemyStats.health;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemyStats.moveSpeed;

        playerBase = GameObject.FindGameObjectsWithTag("CPU")[0];
        agent.SetDestination(playerBase.transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CPU" && !enemyStats.effectToPlay)
        {
            other.gameObject.GetComponent<IDamageable>().DealDamage(enemyStats.damage);
            KillObject();
        }
        // if the enemy has an effect to play on death (adware) kill it and play the effect
        else if (other.gameObject.tag == "CPU" && enemyStats.effectToPlay)
        {
            other.gameObject.GetComponent<IDamageable>().DealDamage(enemyStats.damage);
            playEffect.Invoke();
        }
    }

    public void KillObject()
    {
        Destroy(gameObject);
    }

    // Interfaces

    public void DealDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            KillObject();
        }
    }
}
