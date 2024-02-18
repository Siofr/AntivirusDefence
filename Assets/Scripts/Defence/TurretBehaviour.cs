using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    // Import the scriptable object
    [SerializeField] private TurretStats stats;

    // List of enemies
    private List<GameObject> enemyList;

    private GameObject target;
    private IDamageable targetInterface;

    private float nextShot;

    // Laser Sight
    private LineRenderer laserSight;

    void Awake()
    {
        GetComponent<SphereCollider>().radius = stats.defenceRange;
        laserSight = GetComponent<LineRenderer>();
        laserSight.SetPosition(0, transform.position);
        enemyList = new List<GameObject>();
    }

    void Update()
    {
        // If the enemy List is greater than 0, meaning an enemy is present
        if (enemyList.Count - 1 > 0)
        {
            // If the first item of the list is null remove it (Cleans up killed enemies)
            if (enemyList[0] == null)
            {
                enemyList.RemoveAt(0);
            }

            // If the turret has a target shoot at it, or else get a new target from the first position on the list
            if (target != null)
            {
                laserSight.enabled = true;
                laserSight.SetPosition(1, target.transform.position);
                Fire(targetInterface);
            }
            else
            {
                target = enemyList[0];
                targetInterface = target.gameObject.GetComponent<IDamageable>();
            }
        }
        else
        {
            laserSight.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If an enemy enters the range of the turret at it to a list
        if (other.gameObject.tag == "Enemy")
        {
            // Enemy Found
            enemyList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If an enemy exits the range of the turret remove it from the list
        if (other.gameObject.tag == "Enemy")
        {
            enemyList.Remove(other.gameObject);
        }
    }

    void Fire(IDamageable target)
    {
        // Rate of fire stuff
        if (Time.time > nextShot)
        {
            nextShot = Time.time + stats.defenceFireRate;

            // Deal damage to the target
            target.DealDamage(stats.defenceDamage);
        }
    }
}
