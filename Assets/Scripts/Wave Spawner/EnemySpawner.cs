using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    // How often enemies spawn
    [SerializeField] private float spawnInterval;
    private float nextSpawn;

    private Transform enemySpawners;
    private List<Vector3> spawnLocations = new List<Vector3>();

    public UnityEvent waveOver;

    // How much spawner has to spend on enemies, cost increases as wave increases
    [SerializeField] private int startEnemyBudget;
    [SerializeField] private int difficultyIncrease;
    private int currentEnemyBudget;

    private int currentWave = 1;

    // Enemies that can be spawned, spawner will select from this dictionary when spawning enemies, int indicates the cost to spawn said enemy
    [SerializeField] private List<GameObject> unlockedEnemyList = new List<GameObject>();

    // Enemies that can not be spawned, these will be harder enemies that will be added to the unlocked enemy dictionary as difficulty increases
    [SerializeField] private List<GameObject> lockedEnemyList = new List<GameObject>();

    // I'm doing this so I can avoid using GetComponent everytime I spawn an enemy cus GetComponent can be expensive
    public List<int> enemyCosts = new List<int>();
    public List<float> enemyWeights = new List<float>();

    int lockedEnemyListIndex;

    void Awake()
    {
        currentEnemyBudget = startEnemyBudget;

        lockedEnemyListIndex = 0;

        // Getting the spawn location's parent
        enemySpawners = gameObject.transform;

        // Adding the positions to the spawn locations list
        foreach (Transform spawner in enemySpawners)
        {
            spawnLocations.Add(spawner.position);
        }

        // Populating Cost List
        for (int i = 0; i < unlockedEnemyList.Count; i++)
        {
            enemyCosts.Add(unlockedEnemyList[i].GetComponent<EnemyBehaviour>().enemyStats.enemyCost);
            enemyWeights.Add(unlockedEnemyList[i].GetComponent<EnemyBehaviour>().enemyStats.spawnWeight);
        }

        for (int i = 0; i < lockedEnemyList.Count; i++)
        {
            enemyCosts.Add(lockedEnemyList[i].GetComponent<EnemyBehaviour>().enemyStats.enemyCost);
            enemyWeights.Add(lockedEnemyList[i].GetComponent<EnemyBehaviour>().enemyStats.spawnWeight);
        }
    }

    void Update()
    {
        if (currentEnemyBudget > 0)
        {
            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnInterval;
                int randomIndex = GetRandomWeightedIndex(enemyWeights.GetRange(0, unlockedEnemyList.Count));
                Debug.Log(randomIndex);
                Instantiate(unlockedEnemyList[randomIndex], spawnLocations[Random.Range(0, spawnLocations.Count)], Quaternion.identity);
                currentEnemyBudget -= enemyCosts[randomIndex];
            }
        }

        else
        {
            waveOver.Invoke();
        }
    }

    public int GetRandomWeightedIndex(List<float> weights)
    {
        float weightSum = 0f;
        int index = 0;
        int lastIndex = weights.Count - 1;

        for (int i = 0; i < weights.Count; ++i)
        {
            weightSum += weights[i];
        }

        while (index < lastIndex)
        {
            if (Random.Range(0, weightSum) < weights[index])
            {
                return index;
            }

            weightSum -= weights[index++];
        }

        return index;
    }


    void UnlockEnemy()
    {
        // If there are still locked enemies add the next one to the list
        if (lockedEnemyListIndex != lockedEnemyList.Count)
        {
            unlockedEnemyList.Add(lockedEnemyList[lockedEnemyListIndex]);
            lockedEnemyListIndex++;
        }
    }

    public void StartNewWave()
    {
        currentEnemyBudget = startEnemyBudget;
        currentWave++;

        // Every five waves a new enemy gets added to the list of possible spawns
        if (currentWave % 1 == 0)
        {
            UnlockEnemy();
        }
    }
}
