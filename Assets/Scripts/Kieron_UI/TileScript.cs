using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileScript : MonoBehaviour
{
    public GameObject tileUI;
    //public GameObject upgradeUI;
    public UIScript UI;
    public GameObject tower;
    public Transform towerTransform;
    //public EconomySystem economy;
    //public TurretStats turret, raygun;
    public TurretStats stats;
    //public TMP_Text turretCost, raygunCost;
    public TMP_Text description;
    private bool built = false;
    
    // Start is called before the first frame update
    void Start()
    {
        //turretCost.text = turret.defenceCost.ToString();
        //raygunCost.text = raygun.defenceCost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(tower == null && built == true)
        {
            built = false;

            int cost = (stats.defenceCost) / 2;

            stats = null;

            EconomySystem.cryptocoins += cost;
        }
    }

    public void BuildTower(GameObject chosenTower)
    {
        if(stats == null || stats != chosenTower.GetComponent<DefenceStats>().defenceStats)
        {
            stats = chosenTower.GetComponent<DefenceStats>().defenceStats;

            UI.target = chosenTower;

            UI.targetEffect.text = stats.defenceDescription;
        }
        else
        {
            if(tower == null)
            {
                int coins = EconomySystem.cryptocoins;
                int cost = chosenTower.GetComponent<DefenceStats>().defenceStats.defenceCost;

                if(coins >= cost)
                {
                    EconomySystem.cryptocoins -= chosenTower.GetComponent<DefenceStats>().defenceStats.defenceCost;
                    tower = Instantiate(chosenTower, towerTransform);
                    tileUI.SetActive(false);
                    UI.target = tower;

                    UI.targetEffect.text = chosenTower.GetComponent<DefenceStats>().defenceStats.defenceDescription;

                    built = true;
                }
                else
                {
                    UI.targetEffect.text = "Not Enough Cryptocoins!";
                }
            }
        }
    }

    public void SellTower()
    {
        
    }
}
