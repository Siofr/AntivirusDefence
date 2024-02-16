using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPrefab : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    public void DestroySource()
    {
        audioClip = GetComponent<AudioSource>().clip;
        Destroy(gameObject, audioClip.length);
    }
}
