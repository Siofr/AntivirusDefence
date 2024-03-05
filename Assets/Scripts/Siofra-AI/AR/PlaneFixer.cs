using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneFixer : MonoBehaviour
{
    [SerializeField] private Transform newParent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Trackables") == null)
        {
            return;
        }
        else
        {
            GameObject.Find("Trackables").transform.SetParent(newParent);
            Destroy(gameObject);
        }
    }
}
