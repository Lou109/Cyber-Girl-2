using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float runSpeed;
    Rigidbody2D myRigidbody;
    [SerializeField] BoxCollider2D myboxCollider;
    Health health;
    [SerializeField] Animator animator;
    [SerializeField] PolygonCollider2D childpolygon;


    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myboxCollider = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        health = GetComponent<Health>();
        childpolygon = GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        MoveEnemy();
        AfterPlayerDies();
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
            childpolygon.enabled = false;
            myRigidbody.velocity = Vector2.zero;
            animator.SetTrigger("isdogDying");
            
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            moveSpeed = -runSpeed;
            bool enemyHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
            animator.SetBool("isdogRunning", enemyHasHorizontalSpeed);
            
            myboxCollider.enabled = false;
        }
    }

    void AfterPlayerDies()
    {
        if (childpolygon.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            myRigidbody.velocity = Vector2.zero;
            animator.SetTrigger("isdogBites");
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