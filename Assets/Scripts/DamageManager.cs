using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{ // burasý daha iyi ayarlanar bilir düz vuruþa farklý damage falan ona göre isimleride ayarlarsýn   
    float playerHitTimer = 1f;
    float enemyHitTimer = 0.5f; // For Touching Damage ---
    float timeToChangeColor = 0.4f;
    float enemySmallKnockback = 60f;
    float enemyBigKnockback = 85f;

    bool playerCanTakeDamage = true;
    bool enemyCanTakeDamage = true;
    bool enemyCanChangeColor = true;

    Color getHitColor = new Color(0, 0, 0);

    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    EnemyMovement enemyMovement;

    [SerializeField] float playerMeleeDamage = 10f;
    [SerializeField] float enemyMeleeDamage = 10f;
    [SerializeField] float touchingDamage = 50f;
    //[SerializeField] EnemyHealth enemyHealth;

    public void DamageEnemy(GameObject other)
    {
        enemyMovement = other.GetComponent<EnemyMovement>();
        enemyMovement.EnemyKnockBack(enemySmallKnockback);

        enemyHealth = other.GetComponent<EnemyHealth>();
        enemyHealth.health -= playerMeleeDamage;        
        
        enemyHealth.Death();
        if (other != null && enemyCanChangeColor)
        {
            enemyHealth.enemySR.color = getHitColor;
            enemyCanChangeColor = false;
            StartCoroutine(WaitForThing(timeToChangeColor)); // Renk Deðiþtirme ---
        }
        if (enemyHealth.isEnemyDeath)
            enemyCanChangeColor = true;
    }

    public void DamagePlayer(GameObject other) // daha enemyCombat yok o yüzden burasý þimdilik böyle ileride inþ bozulmaz bu kod :((((
    {
        if(playerCanTakeDamage)
        {
            playerCanTakeDamage = false;
            playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.health -= enemyMeleeDamage;
            playerHealth.ManagePlayerHealth();
            StartCoroutine(WaitForThing(playerHitTimer)); // Time For Player To Get Hit Again ---
        }
    }

    public void DamagePlayerByTouching(GameObject other)
    {
        if (playerCanTakeDamage)
        {
            playerCanTakeDamage = false; // bak
            playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.health -= touchingDamage;
            playerHealth.ManagePlayerHealth();
            StartCoroutine(WaitForThing(playerHitTimer)); // Time For Player To Get Hit Again ---
        }      
    }

    public void DamageEnemyByTouching(GameObject other)
    {
        if (enemyCanTakeDamage)
        {
            enemyCanTakeDamage = false;
            enemyMovement = other.GetComponent<EnemyMovement>();
            enemyMovement.EnemyKnockBack(enemyBigKnockback);

            enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.health -= touchingDamage;
            
            enemyHealth.Death();
            if (other != null)
            {
                if(enemyCanChangeColor)
                {
                    enemyHealth.enemySR.color = getHitColor;
                    enemyCanChangeColor = false;
                    StartCoroutine(WaitForThing(timeToChangeColor)); // Renk Deðiþtirme ---
                }                   
                StartCoroutine(WaitForThing(enemyHitTimer)); // Enemy Hit Timer ---
            }
            if (enemyHealth.isEnemyDeath) // - Enemy Renk Deðiþmeme Sorunu Çözüldü ---
                enemyCanChangeColor = true;
        }      
    }

    IEnumerator WaitForThing(float waitTime) 
    {
        yield return new WaitForSeconds(waitTime);
        if (waitTime == timeToChangeColor && !enemyHealth.isEnemyDeath)
        {
            enemyCanChangeColor = true;
            enemyHealth.enemySR.color = enemyHealth.enemyFirstColor;
        }
        else if (waitTime == playerHitTimer)
            playerCanTakeDamage = true;
        else
            enemyCanTakeDamage = true;
    }
}
