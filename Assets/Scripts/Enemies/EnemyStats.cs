using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyStats", order = 2)]
public class EnemyStats : ScriptableObject
{
    public string enemyName;
    public bool effectToPlay;
    public int enemyCost;
    public float moveSpeed;
    public float health;
    public float damage;
}
