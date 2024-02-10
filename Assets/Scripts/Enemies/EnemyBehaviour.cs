using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyStats enemyStats;
    private NavMeshAgent agent;
    private GameObject playerBase;
    private float health;

    void Awake()
    {
        health = enemyStats.health;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemyStats.moveSpeed;

        playerBase = GameObject.FindWithTag("CPU");
        agent.SetDestination(playerBase.transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CPU")
        {
            other.gameObject.GetComponent<IDamageable>().DealDamage(enemyStats.damage);
            KillObject();
        }
    }

    public void DealDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            KillObject();
        }
    }

    public void KillObject()
    {
        Destroy(gameObject);
    }
}
