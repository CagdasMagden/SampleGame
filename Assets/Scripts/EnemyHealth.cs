using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public SpriteRenderer enemySR;
    public Color enemyFirstColor;
    public float health = 100;
    public bool isEnemyDeath = false;

    public void Death()
    {       
        if(health <= 0)
        {
            isEnemyDeath = true;
            Destroy(gameObject);
        }        
    }

    private void Start()
    {
        enemySR = GetComponent<SpriteRenderer>();
        enemyFirstColor = enemySR.color;
    }

    public Color GetEnemyColor()
    {
        return enemySR.color;
    }
}
