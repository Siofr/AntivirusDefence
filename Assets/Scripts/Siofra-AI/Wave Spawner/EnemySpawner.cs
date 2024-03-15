using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    public bool gameStarted = false;

    // How often enemies spawn
    [SerializeField] private float spawnInterval;
    private float nextSpawn;

    private Transform enemySpawners;
    [SerializeField] private List<Transform> spawnTransforms = new List<Transform>();
    private List<Vector3> spawnPositions = new List<Vector3>();

    [SerializeField] private Transform leftPathTransform;
    [SerializeField] private Transform rightPathTransform;

    private List<Vector3> leftPath = new List<Vector3>();
    private List<Vector3> rightPath = new List<Vector3>();

    public UnityEvent waveOver;

    // How much spawner has to spend on enemies, cost increases as wave increases
    [SerializeField] private int startEnemyBudget;
    [SerializeField] private int difficultyIncrease;
    public int currentEnemyBudget;

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
        foreach (Transform spawner in spawnTransforms)
        {
            spawnPositions.Add(spawner.position);
        }

        foreach (Transform point in leftPathTransform)
        {
            leftPath.Add(point.position);
        }

        foreach (Transform point in rightPathTransform)
        {
            rightPath.Add(point.position);
        }

        // Populating Cost and Weights List
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
        // If the spawner has "currency" to spend on enemies
        if (currentEnemyBudget > 0 && gameStarted)
        {
            // If the time is greater than the next spawn then spawn an enemy
            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnInterval;

                // Get a random weighted index, and instantiate the enemy at that index in the unlocked enemy list
                int randomIndex = GetRandomWeightedIndex(enemyWeights.GetRange(0, unlockedEnemyList.Count));
                int randomSpawn = Random.Range(0, spawnPositions.Count);
                GameObject enemy = Instantiate(unlockedEnemyList[randomIndex], spawnPositions[randomSpawn], Quaternion.identity);

                if (randomSpawn == 1)
                {
                    EnemyBehaviour enemyScript = enemy.GetComponent<EnemyBehaviour>();
                    enemyScript.enemyPath = leftPath;
                    enemyScript.targetPosition = leftPath[0];
                }
                else
                {
                    EnemyBehaviour enemyScript = enemy.GetComponent<EnemyBehaviour>();
                    enemyScript.enemyPath = rightPath;
                    enemyScript.targetPosition = rightPath[0];
                }

                // Deduct from the spawner's currency
                currentEnemyBudget -= enemyCosts[randomIndex];
            }
        }

        // If it doesn't we end the wave
        else if (currentEnemyBudget <= 0 && gameStarted)
        {
            waveOver.Invoke();
        }
    }

    // This returns a random weighted int
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
        // Increase the spawner's budget to spend on enemies based on the current wave and increase the wave
        currentEnemyBudget = startEnemyBudget + (difficultyIncrease * currentWave);
        currentWave++;

        // Every two waves a new enemy gets added to the list of possible spawns
        if (currentWave % 1 == 0)
        {
            UnlockEnemy();
        }
    }

    public void StartGame()
    {
        gameStarted = true;
    }
}
