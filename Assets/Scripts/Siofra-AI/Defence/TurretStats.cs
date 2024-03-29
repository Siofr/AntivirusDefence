using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TurretStats", order = 1)]
public class TurretStats : ScriptableObject
{
    public string defenceName;
    public string defenceDescription;
    public float defenceHealth;
    public int defenceCost;
    public float defenceRange;
    public float defenceDamage;
    public float defenceSplashRadius;
    public float defenceFireRate;
    public float chargeTime;
}