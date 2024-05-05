using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    float wallTouchCheckRadius = 0.2f;
    float wallJumpDirection;
    //bool buy = false; ----- ilerde kullanýlabilir
    bool isWallSliding;
    bool canRun = true; //deneme ----- baþarýlý ----- daha iyi kod yazýlabilir  
    bool canJumpAfterWJ;

    bool doubleJump;
    bool haveHermes;

    Vector2 moveInput;

    MovePlatforms movePlatforms;
    Rigidbody2D playerRB;
    CapsuleCollider2D playerBodyCollider;
    BoxCollider2D playerFeetCollider;

    ShowItemsInMenu showItemsInMenu;
    [SerializeField] GameObject itemVisual;

    [SerializeField] BoxCollider2D mushroomHitBox;
    [SerializeField] float mushroomJumpPower;

    [SerializeField] float smoothingForAfterWJ;
    [SerializeField] float delayForRunAfterWJ;
    [SerializeField] float wallJumpForceX;
    [SerializeField] float wallJumpForceY;
    [SerializeField] float fallSpeedMultiplayer = 1.5f;
    [SerializeField] float wallSlidingSpeed = 1f;
    [SerializeField] float playerRunSpeed;
    [SerializeField] float playerJumpSpeed;
    [SerializeField] Transform wallTouching;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerBodyCollider = GetComponent<CapsuleCollider2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
        showItemsInMenu = itemVisual.GetComponent<ShowItemsInMenu>();
        mushroomHitBox.name = "mushroomHitBox";
    }

    void Update()
    {
        Run();
        FlipSprite();
        WallSliding();
        FallFaster();
        GetItemBuffs(showItemsInMenu.itemNames);
        //Debug.Log(haveHermes); true
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void Run()
    {
        if (canRun)
        {
            Vector2 playerHorizontalMove = new Vector2(moveInput.x * playerRunSpeed, playerRB.velocity.y);
            playerRB.velocity = playerHorizontalMove;
        }
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(playerRB.velocity.x) > 0;
        if (playerHasHorizontalSpeed)
            transform.localScale = new Vector2(Mathf.Sign(playerRB.velocity.x), transform.localScale.y); // lower case mathf
    }

    void OnJump(InputValue value)
    {
        if (IsTouchingGround() && value.isPressed)
        {
            Jump();
            doubleJump = true;            
        }
        else if (doubleJump && value.isPressed && haveHermes && !IsTouchingGround())
        {
            Jump();
            doubleJump = false;
        }

        if (isWallSliding && value.isPressed)
        {
            WallJump();
            canJumpAfterWJ = true;           
        }
        else if (canJumpAfterWJ && value.isPressed && haveHermes && !isWallSliding)
        {
            Jump();
            canJumpAfterWJ = false;
        }
    }

    void Jump()
    {
        playerRB.velocity = new Vector2(0, 0);
        Vector2 playerVerticalMove = new Vector2(0f, playerJumpSpeed);
        playerRB.velocity += playerVerticalMove;
    }

    void FallFaster()
    {
        if (playerRB.velocity.y < 0f)
        {
            Vector2 currentVectorForFasterFall = new Vector2(playerRB.velocity.x, -playerRB.velocity.y);
            playerRB.velocity -= currentVectorForFasterFall * fallSpeedMultiplayer * Time.deltaTime;
        }
    }

    void WallJump()
    {
        wallJumpDirection = -transform.localScale.x;
        playerRB.velocity = new Vector2(wallJumpDirection * wallJumpForceX, wallJumpForceY);//run sürekli çalýþtýðý için ona takýlýyor --- hallod glba
        canRun = false;
        StartCoroutine(StopRunAfterWJ());
    }

    IEnumerator StopRunAfterWJ() //deneme ------ baþarýlý------ daha iyi bir kod yazýlabilir --------yarýn daha iyisi olacak --- oldu glba
    {
        yield return new WaitForSeconds(delayForRunAfterWJ);
        playerRB.velocity = new Vector2(playerRB.velocity.x, smoothingForAfterWJ);
        canRun = true;
    }

    bool IsTouchingWall()
    {
        return Physics2D.OverlapCircle(wallTouching.position, wallTouchCheckRadius, LayerMask.GetMask("Ground"));
    }

    bool IsTouchingGround()
    {
        return playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    void WallSliding()//bunu satýnalýnabilir bi özellik yap yada direkt walljumpuda ekle BURASIEÞYAYLA ALINACAK
    {
        if (!IsTouchingGround() && IsTouchingWall()) // buraya sadce a ve d ye basýlý tutarken true olacak þekilde ekle
        {
            isWallSliding = true;
            playerRB.velocity = new Vector2(playerRB.velocity.x, Mathf.Clamp(playerRB.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
            isWallSliding = false;
    }

    void GetItemBuffs(List<string> nameItem)
    {
        if (nameItem.Contains("Hermes Boots"))
            haveHermes = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.name == "mushroomHitBox" && playerFeetCollider.IsTouching(other.collider))
        {
            Vector2 mushroomPushPower = new Vector2(0f, mushroomJumpPower);
            playerRB.velocity = mushroomPushPower;
            doubleJump = true;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            movePlatforms = other.gameObject.GetComponent<MovePlatforms>();
            transform.position += new Vector3(movePlatforms.moveSpeed * movePlatforms.whichWay * Time.deltaTime, 0, 0);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(wallTouching.position, wallTouchCheckRadius);
    }
}
