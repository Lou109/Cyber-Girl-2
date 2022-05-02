using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidbody;
    CapsuleCollider2D capsuleCollider;
    Health health;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    void Start()
    {
        health = GetComponent<Health>();
    }

    void Update()
    {
        MoveEnemy();
    }
    public void MoveEnemy()
    {
        int healthAmount = health.GetHealth();


        if (healthAmount >= 0)
        {
            myRigidbody.velocity = new Vector2(moveSpeed, 0f);
        }

        else if (healthAmount <= 0)
        {
            myRigidbody.velocity = Vector2.zero;
            capsuleCollider.enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            moveSpeed = -moveSpeed;
            FlipEnemyFacing();
        }

        void FlipEnemyFacing()
        {
            transform.localScale = new Vector2(-Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
}