using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class WaveState : MonoBehaviour
{
    [SerializeField] private GameObject downtimeUI;
    public UnityEvent startWave;

    public void StartNextWave()
    {
        downtimeUI.SetActive(false);
        startWave.Invoke();
    }

    public void StartDowntime()
    {
        downtimeUI.SetActive(true);
    }
}
