using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TurretStats", order = 1)]
public class TurretStats : ScriptableObject
{
    public string defenceName;
    public float defenceRange;
    public float defenceDamage;
    public float defenceFireRate;
}