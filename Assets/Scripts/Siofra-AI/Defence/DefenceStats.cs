using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceStats : MonoBehaviour
{
    public TurretStats defenceStats;
    public GameObject upgradeUI;
    public float health;

    void Start()
    {
        health = defenceStats.defenceHealth;
    }

    public void SellTower()
    {
        Destroy(gameObject);
    }

    public void UpgradeTower()
    {
        
    }
}
