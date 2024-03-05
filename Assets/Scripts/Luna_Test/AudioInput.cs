using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInput : MonoBehaviour
{
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
            pause = false;
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
        int counter = 3;
        while (true)
        {
            if (counter != 0)
            {
                adwareStats.moveSpeed = 0f;
                enemyStats.moveSpeed = 0f;
            }
            else
            {
                adwareStats.moveSpeed = 0.75f;
                enemyStats.moveSpeed = 1f;
                break;
            }
            counter--;
            yield return new WaitForSeconds(1.0f);
        }
    }
}
