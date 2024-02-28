using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileScript : MonoBehaviour
{
    public GameObject tileUI;
    public UIScript UI;
    public GameObject tower;
    public Transform towerTransform;
    //public EconomySystem economy;
    public TurretStats turret, raygun;
    public TMP_Text turretCost, raygunCost;
    
    // Start is called before the first frame update
    void Start()
    {
        turretCost.text = turret.defenceCost.ToString();
        raygunCost.text = raygun.defenceCost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuildTower(GameObject chosenTower)
    {
        if(tower == null)
        {
            int coins = EconomySystem.cryptocoins;
            int cost = chosenTower.GetComponent<DefenceStats>().defenceStats.defenceCost;

            if(coins >= cost)
            {
                EconomySystem.cryptocoins -= chosenTower.GetComponent<DefenceStats>().defenceStats.defenceCost;
                tower = Instantiate(chosenTower, towerTransform);
                UI.target = tower;
                tileUI.SetActive(false);
            }
        }
    }
}
