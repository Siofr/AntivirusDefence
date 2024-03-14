using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInput : MonoBehaviour
{
    public EMPTowerBehaviour empTower;
    public AudioSource source;
    public AudioDetection audioDetection;

    public EnemyStats adwareStats;
    public EnemyStats enemyStats;
    public bool pause = false;

    public float loudnessSensibility = 100;
    public float threshold = 50f;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(EnemyStop());
    }

    // Update is called once per frame
    void Update()
    {
        float loudness = audioDetection.GetLoudnessFromMicrophone() * loudnessSensibility;

        if (loudness < threshold)
        {
            loudness = 0;
        }

        else if (loudness > threshold)
        {
            pause = true;
            StartCoroutine(EnemyStop());
        }
    }

    IEnumerator EnemyStop()
    {
        if(pause == true)
        {
            int counter = 3;
            while (true)
            {
                foreach (GameObject enemy in empTower.enemyList)
                {
                    EnemyBehaviour enemyScript = enemy.GetComponent<EnemyBehaviour>();
                    if (counter != 0)
                    {
                        enemyScript.speed = 0f;
                    }
                    else
                    {
                        enemyScript.speed = enemyStats.moveSpeed;
                        pause = false;
                        break;
                    }
                }
                counter--;
                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}