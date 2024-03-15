using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInput : MonoBehaviour
{
    public EMPTowerBehaviour empTower;
    public AudioSource source;
    public AudioDetection audioDetection;

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
                    if (counter != 0)
                    {
                        foreach (GameObject enemy in empTower.targetList)
                        {
                            EnemyBehaviour enemyScript = enemy.GetComponent<EnemyBehaviour>();
                            enemyScript.speed -= enemyScript.speed;
                        }
                    }
                    else
                    {
                        foreach (GameObject enemy in empTower.targetList)
                        {
                            EnemyBehaviour enemyScript = enemy.GetComponent<EnemyBehaviour>();
                            enemyScript.speed = enemyScript.temp;
                        }
                        pause = false;
                        break;
                    }
                counter--;
                yield return new WaitForSeconds(1.0f);
            }
        }
        StopCoroutine(EnemyStop());
    }
}