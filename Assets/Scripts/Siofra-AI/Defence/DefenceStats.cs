using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceStats : MonoBehaviour
{
    public TurretStats defenceStats;
    public float health;

    void Start()
    {
        health = defenceStats.defenceHealth;
    }
}
