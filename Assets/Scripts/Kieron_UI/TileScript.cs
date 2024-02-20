using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public GameObject tileUI;
    public UIScript UI;
    private GameObject tower;
    public Transform towerTransform;
    public EconomySystem economy;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuildTower(GameObject chosenTower)
    {
        if(tower == null)
        {
            int coins = economy.cryptocoins;
            int cost = chosenTower.GetComponent<TowerScript>().cost;

            if(coins >= cost)
            {
                economy.cryptocoins =- chosenTower.GetComponent<TowerScript>().cost;
                tower = Instantiate(chosenTower, towerTransform);
                UI.target = tower;
            }
        }
    }
}
