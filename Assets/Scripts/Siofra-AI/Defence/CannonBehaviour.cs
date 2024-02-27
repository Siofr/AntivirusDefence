using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBehaviour : MonoBehaviour
{
    // Import the scriptable object
    private TurretStats stats;

    // List of enemies
    public List<GameObject> enemyList;

    public GameObject target;
    private IDamageable targetInterface;

    private float nextShot;

    // Bullet Trail
    private LineRenderer bulletTrail;
    private Vector3 trailSpawnPosition;
    [SerializeField] private Transform trailSpawnObject;
    [SerializeField] private float trailLifetime;

    void Awake()
    {
        stats = GetComponent<DefenceStats>().defenceStats;
        GetComponent<CapsuleCollider>().radius = stats.defenceRange;

        trailSpawnPosition = trailSpawnObject.position;
        bulletTrail = GetComponent<LineRenderer>();
        bulletTrail.SetPosition(0, transform.position);
        enemyList = new List<GameObject>();
    }

    void Update()
    {
        // If the enemy List is greater than 0, meaning an enemy is present
        if (enemyList.Count > 0)
        {
            // If the first item of the list is null remove it (Cleans up killed enemies)
            if (enemyList[0] == null)
            {
                enemyList.RemoveAt(0);
            }

            // If the turret has a target shoot at it, or else get a new target from the first position on the list
            if (target != null)
            {
                Fire(targetInterface, target.gameObject);
            }
            // This prevents index error
            else if (target == null && enemyList.Count > 0)
            {
                target = enemyList[0];
                targetInterface = target.gameObject.GetComponent<IDamageable>();
            }
        }
        else
        {
            bulletTrail.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Cannon trigger");
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
            target = null;
        }
    }

    void Fire(IDamageable target, GameObject targetObject)
    {
        // Rate of fire stuff
        if (Time.time > nextShot)
        {
            Debug.Log("Cannon Fired");
            nextShot = Time.time + stats.defenceFireRate;

            StartCoroutine(BulletTrail(targetObject.transform.position));

            // Deal damage to the target
            Vector3 explosionPosition = targetObject.transform.position;

            Collider[] explosionColliders = Physics.OverlapSphere(explosionPosition, stats.defenceSplashRadius);

            foreach (Collider explosionCollider in explosionColliders)
            {
                if (explosionCollider.transform.gameObject.tag == "Enemy")
                {
                    explosionCollider.transform.GetComponent<IDamageable>().DealDamage(stats.defenceDamage);
                }
            }
        }
    }

    private IEnumerator BulletTrail(Vector3 targetPos)
    {
        Vector3[] trailPositions = new Vector3[2];

        trailPositions[0] = trailSpawnPosition;
        trailPositions[1] = targetPos;

        bulletTrail.SetPositions(trailPositions);
        bulletTrail.enabled = true;

        yield return new WaitForSeconds(trailLifetime);

        bulletTrail.enabled = false;
    }
}
