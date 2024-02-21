using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    public float spawnTime;
    public GameObject playArea;

    private bool isSpawned = false;

    // Update is called once per frame
    void Update()
    {
        if (spawnTime > Time.time && !isSpawned)
        {
            Instantiate(playArea, Vector3.zero, Quaternion.identity);
            isSpawned = true;
        }
    }
}
