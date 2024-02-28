using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyStats", order = 2)]
public class EnemyStats : ScriptableObject
{
    public string enemyName;
    public string enemyDescription;
    public bool effectToPlay;
    public int enemyCost;
    public float spawnWeight;
    public float moveSpeed;
    public float health;
    public float damage;
    public float enemyFireRate;
    public int coinDrop;
}
