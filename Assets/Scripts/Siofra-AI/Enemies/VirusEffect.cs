using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusEffect : MonoBehaviour
{
    [SerializeField] private GameObject virusPrefab;

    public int currentSplit;
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
        if (currentSplit <= maxSplit)
        {
            Debug.Log("Successful Split Number: " + currentSplit.ToString());
            for (int i = 1; i >= 0; i--)
            {
                GameObject splitEnemy = Instantiate(virusPrefab);
                EnemyBehaviour splitEnemyScript = splitEnemy.GetComponent<EnemyBehaviour>();

                VirusEffect splitEnemyEffectScript = splitEnemy.GetComponent<VirusEffect>();
                splitEnemyEffectScript.currentSplit = currentSplit + 1;

                splitEnemyScript.enemyPath = enemyBehaviourScript.enemyPath;
                splitEnemyScript.currentWaypoint = enemyBehaviourScript.currentWaypoint;
                splitEnemyScript.targetPosition = enemyBehaviourScript.targetPosition;

                splitEnemyScript.health = enemyBehaviourScript.health / splitEnemyEffectScript.currentSplit;
            }
        } 
        else
        {
            Debug.Log("Failed Split Number: " + currentSplit.ToString());
            Debug.Log("Whuh");
        }
    }
}
