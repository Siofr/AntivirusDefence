using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEffect : MonoBehaviour
{
    [SerializeField] private float spawnInterval;
    [SerializeField] private GameObject wormPrefab;
    [SerializeField] private EnemyBehaviour enemyScript;
    private float nextSpawn;
    private bool hasReplicated = false;

    public Collider[] colliders;
    public float radius;

    private void Awake()
    {
        nextSpawn = spawnInterval + Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextSpawn < Time.time && !hasReplicated)
        {
            GameObject wormSpawn = Instantiate(wormPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.05f), Quaternion.identity);

            EnemyBehaviour wormSpawnScript = wormSpawn.GetComponent<EnemyBehaviour>();

            wormSpawnScript.enemyPath = enemyScript.enemyPath;
            wormSpawnScript.currentWaypoint = enemyScript.currentWaypoint;
            wormSpawnScript.targetPosition = enemyScript.targetPosition;

            hasReplicated = true;
        }
    }
}
