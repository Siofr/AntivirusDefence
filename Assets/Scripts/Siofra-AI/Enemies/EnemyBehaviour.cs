using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using Unity.VisualScripting;

public class EnemyBehaviour : MonoBehaviour, IDamageable
{
    public EnemyStats enemyStats;
    public UnityEvent playEffect;
    public List<Vector3> enemyPath = new List<Vector3>();
    public float health;
    public float speed;
    public float temp;

    public Vector3 targetPosition;
    public int currentWaypoint = 0;

    public EconomySystem economy;

    void Awake()
    {
        health = enemyStats.health;
        speed = enemyStats.moveSpeed;
        temp = speed;
        // agent = GetComponent<NavMeshAgent>();
        // agent.speed = enemyStats.moveSpeed;

        // playerBase = GameObject.FindGameObjectsWithTag("CPU")[0];
        // agent.SetDestination(playerBase.transform.position);
    }

    void Update()
    {
        Walk();
    }

    private void Walk()
    {
        transform.forward = Vector3.RotateTowards(transform.forward, targetPosition - transform.position, 60f, 0.0f);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            currentWaypoint++;
            targetPosition = enemyPath[currentWaypoint];
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CPU" && !enemyStats.CPUEffectToPlay)
        {
            other.gameObject.GetComponent<IDamageable>().DealDamage(enemyStats.damage);
            DamageCPU();
        }
        // if the enemy has an effect to play on death (adware) kill it and play the effect
        else if (other.gameObject.tag == "CPU" && enemyStats.CPUEffectToPlay)
        {
            other.gameObject.GetComponent<IDamageable>().DealDamage(enemyStats.damage);
            playEffect.Invoke();
        }
    }

    public void DamageCPU()
    {
        Destroy(gameObject);
    }

    public void KillObject()
    {
        if (enemyStats.deathEffectToPlay)
        {
            playEffect.Invoke();
        }
        EconomySystem.cryptocoins += enemyStats.coinDrop;
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

    public void ReduceSpeed()
    {
        speed = 0;
    }

    public void IncreaseSpeed()
    {
        speed = temp;
    }
}
