using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval;
    private float nextSpawn;

    private bool isSpawning = true;

    private Transform enemySpawners;
    private List<Vector3> spawnLocations = new List<Vector3>();

    // How much spawner has to spend on enemies, cost increases as wave increases
    [SerializeField] private int maxEnemyBudget;
    [SerializeField] private int difficultyIncrease;
    private int currentEnemyBudget;

    private int currentWave;

    // Enemies that can be spawned, spawner will select from this list when spawning enemies
    [SerializeField] private List<GameObject> unlockedEnemyList = new List<GameObject>();

    // Enemies that can not be spawned, these will be harder enemies that will be added to the unlocked enemy list as difficulty increases
    [SerializeField] private List<GameObject> lockedEnemyList = new List<GameObject>();

    void Awake()
    {
        // Getting the spawn location's parent
        enemySpawners = gameObject.transform;

        // Adding the positions to the spawn locations list
        foreach (Transform spawner in enemySpawners)
        {
            spawnLocations.Add(spawner.position);
        }
    }

    void Update()
    {
        if (isSpawning && currentEnemyBudget >= 0)
        {
            if (nextSpawn > Time.time)
            {
                nextSpawn = Time.time + spawnInterval;
                Instantiate(unlockedEnemyList[Random.Range(0, unlockedEnemyList.Count - 1)], spawnLocations[Random.Range(0, spawnLocations.Count - 1)], Quaternion.identity);
            }
        }

        // Every five waves a new enemy gets added to the list of possible spawns
        if (currentWave % 5 == 0)
        {
            UnlockEnemy();
        }
    }

    void UnlockEnemy()
    {
        // If there are still locked enemies add the next one to the list
        if (lockedEnemyList.Count != 0)
        {
            unlockedEnemyList.Add(lockedEnemyList[0]);
            lockedEnemyList.RemoveAt(0);
        }
    }
}
