using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrojanEffect : MonoBehaviour
{
    [SerializeField] List<GameObject> commonEnemyList = new List<GameObject>();
    [SerializeField] List<GameObject> rareEnemyList = new List<GameObject>();

    [SerializeField] private int commonEnemiesToSpawn;
    [SerializeField] private int rareEnemiesToSpawn;

    private int numberEnemiesSpawned = 0;
    private EnemyBehaviour pathingScript;

    // Start is called before the first frame update
    void Awake()
    {
        pathingScript = GetComponent<EnemyBehaviour>();
    }

    public void SpawnAttack()
    {
        EnemySpawn(commonEnemyList, commonEnemiesToSpawn);
        EnemySpawn(rareEnemyList, rareEnemiesToSpawn);

        Destroy(gameObject);
    }

    void EnemySpawn(List<GameObject> enemyList, int numberToSpawn)
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            GameObject enemySpawned = Instantiate(enemyList[Random.Range(0, enemyList.Count)], transform.position - transform.forward * (0.05f * numberEnemiesSpawned), Quaternion.identity);
            EnemyBehaviour enemySpawnedScript = enemySpawned.GetComponent<EnemyBehaviour>();

            enemySpawnedScript.targetPosition = pathingScript.targetPosition;
            enemySpawnedScript.currentWaypoint = pathingScript.currentWaypoint;
            enemySpawnedScript.enemyPath = pathingScript.enemyPath;
            numberEnemiesSpawned++;
        }
    }
}
