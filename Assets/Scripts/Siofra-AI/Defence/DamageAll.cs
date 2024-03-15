using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAll : MonoBehaviour
{
    public QuakeTowerBehaviour quakeTower;
    [SerializeField] private float damage;
    [SerializeField] private float abilityCooldown;

    private TurretStats stats;
    private float radius;

    private float nextUse;

    void Awake()
    {
        radius = stats.defenceRange;
    }

    public void TremorEffect()
    {
        if (nextUse < Time.time)
        {
            nextUse = abilityCooldown + Time.time;

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
            foreach (var hitCollider in hitColliders)
            {
                EnemyBehaviour enemyScript = hitCollider.gameObject.transform.GetComponent<EnemyBehaviour>();
                enemyScript.DealDamage(damage);
            }
        }
    }
}
