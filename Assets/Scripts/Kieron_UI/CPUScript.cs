using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUScript : MonoBehaviour
{
    public float CPUTotalHealth, CPUCurrentHealth;
    public GameObject loseScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SubtractHealth(float damageTaken)
    {
        CPUCurrentHealth =- damageTaken;

        if(CPUCurrentHealth <= 0)
        {
            loseScreen.SetActive(true);
            //Or change scene to LoseScene?
        }
    }
}
