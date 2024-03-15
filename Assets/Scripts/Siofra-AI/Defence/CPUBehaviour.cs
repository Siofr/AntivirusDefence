using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CPUBehaviour : MonoBehaviour, IDamageable
{
    public float maxHealth;
    public float health;
    Scene currentScene;

    void Awake()
    {
        health = maxHealth;
        currentScene = SceneManager.GetActiveScene();
    }

    public void DealDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            // SceneManager.LoadScene(currentScene.name);
        }
    }
}
