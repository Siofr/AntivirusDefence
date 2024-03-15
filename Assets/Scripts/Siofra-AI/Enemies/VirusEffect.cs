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
        maxSplit = enemyBehaviourScript.enemyStats.splitTimes;
    }

    public void Split()
    {
        if (currentSplit <= maxSplit)
        {
            for (int i = 1; i <= 2; i++)
            {
                GameObject splitEnemy = Instantiate(virusPrefab, transform.position - transform.forward * (0.05f * i), Quaternion.identity);
                EnemyBehaviour splitEnemyScript = splitEnemy.GetComponent<EnemyBehaviour>();

                VirusEffect splitEnemyEffectScript = splitEnemy.GetComponent<VirusEffect>();
                splitEnemyEffectScript.currentSplit = currentSplit + 1;

                splitEnemyScript.health = enemyBehaviourScript.health / currentSplit;
                splitEnemyScript.enemyPath = enemyBehaviourScript.enemyPath;
                splitEnemyScript.currentWaypoint = enemyBehaviourScript.currentWaypoint;
                splitEnemyScript.targetPosition = enemyBehaviourScript.targetPosition;
            }
        } 
    }
}
