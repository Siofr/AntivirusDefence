using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGunBehaviour : MonoBehaviour
{
    // Import the scriptable object
    private TurretStats stats;

    // List of enemies
    private List<GameObject> enemyList;

    private GameObject target;
    private IDamageable targetInterface;

    private float nextShot;
    private bool isCharged;

    // Laser Sight
    private LineRenderer laserSight;
    [SerializeField] private Transform laserOrigin;

    void Awake()
    {
        stats = GetComponent<DefenceStats>().defenceStats;
        GetComponent<CapsuleCollider>().radius = stats.defenceRange;
        
        laserSight = GetComponent<LineRenderer>();

        enemyList = new List<GameObject>();
    }

    void Update()
    {
        if (enemyList.Count > 0)
        {
            StartCoroutine(ChargeRay());
        }
        else
        {
            laserSight.enabled = false;
            isCharged = false;
        }

        // If the enemy List is greater than 0, meaning an enemy is present
        if (isCharged)
        {
            // If the first item of the list is null remove it (Cleans up killed enemies)
            if (enemyList[0] == null && isCharged)
            {
                enemyList.RemoveAt(0);
            }

            // If the turret has a target shoot at it, or else get a new target from the first position on the list
            if (target != null)
            {
                Vector3 targetPosition = target.transform.position;
                laserSight.enabled = true;
                laserSight.SetPosition(0, laserOrigin.position);
                laserSight.SetPosition(1, targetPosition);
                Vector3 targetDirection = targetPosition - transform.position;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 60f, 0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
                Fire(targetInterface);
            }
            // This prevents index error
            else if (target == null && enemyList.Count > 0)
            {
                target = enemyList[0];
                targetInterface = target.gameObject.GetComponent<IDamageable>();
            }
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

    private IEnumerator ChargeRay()
    {
        yield return new WaitForSeconds(stats.chargeTime);

        isCharged = true;
    }
}
