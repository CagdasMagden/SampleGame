using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 1000;
    public float health;

    public HealthBar healthBar;

    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    void Update()
    {
       // Death();
    }

    void Death()
    {
        if (health <= 0)
            Destroy(gameObject);
    }

    public void ManagePlayerHealth()
    {
        healthBar.SetHealth(health);
    }
}
