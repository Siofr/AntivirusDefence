using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdwareEffect : MonoBehaviour
{
    [SerializeField] private int strength;
    [SerializeField] private AudioClip errorSound;
    private List<GameObject> inactivePopups = new List<GameObject>();
    private Transform adwarePopups;

    void Awake ()
    {
        adwarePopups = GameObject.FindWithTag("Adware").transform;
    }

    void OnDestroy()
    {
        SFXManager.instance.PlaySFX(errorSound, transform, 0.75f);

        foreach (Transform popup in adwarePopups)
        {
            if (!popup.gameObject.activeInHierarchy)
            {
                inactivePopups.Add(popup.gameObject);
            }
        }

        for (int i = 0; i < inactivePopups.Count; i++) {

            if (strength <= 0)
            {
                break;
            }

            int randomIndex = Random.Range(0, inactivePopups.Count - 1);
            inactivePopups[randomIndex].SetActive(true);
            inactivePopups.RemoveAt(randomIndex);

            strength--;
        }
    }
}
