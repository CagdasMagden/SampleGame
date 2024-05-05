using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour // rigidbodyleri kaldýrdým hadi bakalým böyle daha iyi  olabilir --- karakterle bizimki collidleyince 
                                           // ikiside farklý þeyler yapacak
{
    float enemySpeed = 1f;
    //float enemyKnockbackSpeed = 80f; deðiþti baþka yere eklendi
    float offSetForEnemyAss;
    float facingLeftOffSet = 2f;
    float facingRightOffSet = -2f;
    float allCheckRadius = 0.1f;

    bool inSameGround;
    bool inSameHori;
    bool inSameVert;

    GameObject player;
    PlayerMovement playerMovement;

    //Transform playerTransform;-------------------------------- dursun
    Transform enemyTransform; // bu zaten enemyiye takýlý olduðu için yazmamýza gerek yok ama olsun daha okunabilir ilerde belki kýsýlabilir
    [SerializeField] Transform EnemyGroundCheck;
    [SerializeField] Transform EnemyWallCheck;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        //playerTransform = GameObject.FindGameObjectWithTag("Player").transform; ---------------------------dursun
        enemyTransform = GetComponent<Transform>()  ;
        //allChecks = GetComponentInChildren<Transform>(); // herchecki yapan objeyi alýyor ---

        //Vector3 walkRight = new Vector3(enemySpeed * Time.deltaTime, 0f, 0f);
        //Vector3 walkLeft = new Vector3(-enemySpeed * Time.deltaTime, 0f, 0f);
    }
    void Update()
    {
        move();
        FlipEnemyFace();
        ResetOffSetForEnemyAss();
        IsEnemyAssOnGround();
        InRange();
    }

    void move()
    {
        if (InRange())
        {
            WalkTowardPlayer();           
        }
        else
        {
            if (enemyTransform.localScale.x > 0)
                enemyTransform.position += new Vector3(enemySpeed * Time.deltaTime, 0f, 0f);
            else
                enemyTransform.position += new Vector3(-enemySpeed * Time.deltaTime, 0f, 0f);
        }       
    }

    void WalkTowardPlayer()
    {
        if (player.transform.position.x > enemyTransform.position.x)
        {
            enemyTransform.localScale = new Vector2(1, enemyTransform.localScale.y);
            enemyTransform.position += new Vector3(enemySpeed * Time.deltaTime, 0f, 0f);            
        }
                    
        else if (player.transform.position.x < enemyTransform.position.x)
        {
            enemyTransform.localScale = new Vector2(-1, enemyTransform.localScale.y);
            enemyTransform.position += new Vector3(-enemySpeed * Time.deltaTime, 0f, 0f);
        }            
        else
            return;
    }

    void FlipEnemyFace()
    {
        if(IsEnemyHitWall() || !IsEnemyOnGround())
        {
            enemyTransform.localScale = new Vector2(enemyTransform.localScale.x * -1, enemyTransform.localScale.y);
        }
    }

    void ResetOffSetForEnemyAss() // Burasý Çýkarýlýp IsAss Metoduna Eklenebilir --- 
    {
        if (enemyTransform.localScale.x < 0)
            offSetForEnemyAss = facingLeftOffSet;
        else
            offSetForEnemyAss = facingRightOffSet;
    }

    void InSameVertical()
    {
        if (player.transform.position.y >= enemyTransform.position.y - 0.5f &&
            player.transform.position.y <= enemyTransform.position.y + 0.5f)
            inSameVert = true;
        else
            inSameVert = false;
    }

    void InSameHorizontal()
    {
        if (enemyTransform.position.x + 5f >= player.transform.position.x && //iþaret dikkat ---
            enemyTransform.position.x - 5f <= player.transform.position.x)
            inSameHori = true;
        else
            inSameHori = false;
    }
    
    bool InRange()
    {
        InSameVertical();
        InSameHorizontal();
        return(inSameVert && inSameHori);
    }
    bool IsEnemyHitWall()
    {
        return Physics2D.OverlapCircle(EnemyWallCheck.position, allCheckRadius, LayerMask.GetMask("Ground"));
    }

    bool IsEnemyOnGround()
    {
        return Physics2D.OverlapCircle(EnemyGroundCheck.position, allCheckRadius, LayerMask.GetMask("Ground"));
    }

    bool IsEnemyAssOnGround()
    {      
        return Physics2D.OverlapCircle(new Vector3(EnemyGroundCheck.position.x + offSetForEnemyAss,
                                           EnemyGroundCheck.position.y - 0.5f,
                                           EnemyGroundCheck.position.z), allCheckRadius, LayerMask.GetMask("Ground"));
    }

    public void EnemyKnockBack(float enemyKnockBackSpeed)
    {
        if (IsEnemyAssOnGround())
        {
            if (enemyTransform.localScale.x > 0)
                enemyTransform.position += new Vector3(-enemyKnockBackSpeed * Time.deltaTime, 0f, 0f);
            else
                enemyTransform.position += new Vector3(enemyKnockBackSpeed * Time.deltaTime, 0f, 0f);
        }        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(new Vector3(EnemyGroundCheck.position.x + offSetForEnemyAss,
                                          EnemyGroundCheck.position.y - 0.5f,
                                          EnemyGroundCheck.position.z), allCheckRadius);
        Gizmos.DrawWireSphere(EnemyGroundCheck.position, allCheckRadius);
        Gizmos.DrawWireSphere(EnemyWallCheck.position, allCheckRadius);
    }

}
