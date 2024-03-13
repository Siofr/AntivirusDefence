using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusEffect : MonoBehaviour
{
    [SerializeField] private GameObject virusPrefab;
    [SerializeField] public int currentSplit;
    public int maxSplit;

    [SerializeField] private EnemyBehaviour enemyBehaviourScript;

    // Start is called before the first frame update
    void Awake()
    {
        enemyBehaviourScript.health = enemyBehaviourScript.health / currentSplit;
        maxSplit = enemyBehaviourScript.enemyStats.splitTimes;
    }

    public void Split()
    {

        for (int i = 0; i < currentSplit + 1; i++)
        {

            GameObject splitEnemy = Instantiate(virusPrefab);
            EnemyBehaviour splitEnemyScript = splitEnemy.GetComponent<EnemyBehaviour>();

            VirusEffect splitEnemyEffectScript = splitEnemy.GetComponent<VirusEffect>();
            splitEnemyEffectScript.currentSplit = currentSplit++;

            splitEnemyScript.enemyPath = enemyBehaviourScript.enemyPath;
            splitEnemyScript.currentWaypoint = enemyBehaviourScript.currentWaypoint;
            splitEnemyScript.targetPosition = enemyBehaviourScript.targetPosition;

            splitEnemyScript.health = enemyBehaviourScript.health / splitEnemyEffectScript.currentSplit;
            
        }
        Debug.Log("Split");
        Destroy(gameObject);
    }
}
