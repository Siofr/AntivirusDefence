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
        // Find the adware UI Element
        adwarePopups = GameObject.FindGameObjectsWithTag("Adware")[0].transform;
    }

    public void PopupAttack()
    {
        SFXManager.instance.PlaySFX(errorSound, transform, 0.75f);

        // For each popup if the popup is inactive, add it to the inactive popup list
        foreach (Transform popup in adwarePopups)
        {
            if (!popup.gameObject.activeInHierarchy)
            {
                inactivePopups.Add(popup.gameObject);
            }
        }

        // For each popup in the inactive popup list, activate a random one, deduct from the adware's strength, and if that strength is 0 break the loop
        for (int i = 0; i < inactivePopups.Count; i++)
        {

            if (strength <= 0)
            {
                break;
            }

            int randomIndex = Random.Range(0, inactivePopups.Count - 1);
            inactivePopups[randomIndex].SetActive(true);
            inactivePopups.RemoveAt(randomIndex);

            strength--;
        }

        // Destroy the adware enemy
        Destroy(gameObject);
    }
}
