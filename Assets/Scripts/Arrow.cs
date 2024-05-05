using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    float horizontalSpeed;

    Rigidbody2D arrowRB;
    PlayerMovement playermove;

    [SerializeField] float arrowSpeed;
    void Start()
    {
        arrowRB = GetComponent<Rigidbody2D>();
        playermove = FindObjectOfType<PlayerMovement>(); // baþlatta yaptýðýndan performans çok sýkýnýtý deðil
        horizontalSpeed = playermove.transform.localScale.x * arrowSpeed;
        if (horizontalSpeed < 0)
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    void Update()
    {
        arrowRB.velocity = new Vector2(horizontalSpeed, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
            Destroy(other.gameObject);

        Destroy(gameObject);
    }
}
