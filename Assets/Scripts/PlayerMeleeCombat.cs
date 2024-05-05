using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMeleeCombat : MonoBehaviour
{   
    DamageManager damageManager;

    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;

    void Start()
    {
        damageManager = GetComponent<DamageManager>();
    }
    void OnFire(InputValue value)
    {
        if (value.isPressed)
            Attack();
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, LayerMask.GetMask("Enemies")); //farkl� olabilir

        foreach (Collider2D enemies in hitEnemies)
        {
            //Debug.Log("Hello World"); //��zd�m mk
            damageManager.DamageEnemy(enemies.gameObject);
        }       
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            damageManager.DamagePlayerByTouching(gameObject);
            damageManager.DamageEnemyByTouching(other.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other) // bura s�k�nt� ��karabilir unutma --- karakterler bu kadar uzun s�re ya�amayaca��
    {                                      // i�in �uan ask�ya ald�m ---
        if (other.CompareTag("Enemy"))
        {
            damageManager.DamagePlayerByTouching(gameObject);
            damageManager.DamageEnemyByTouching(other.gameObject);
        }
    }
}
