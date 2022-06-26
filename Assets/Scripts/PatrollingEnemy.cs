using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float runSpeed;
    Rigidbody2D myRigidbody;
    BoxCollider2D myboxCollider;
    Health health;
    [SerializeField] Animator animator;
    [SerializeField] PolygonCollider2D childpolygon;
    [SerializeField] Transform[] waypoints;
    int waypointIndex = 0;


    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myboxCollider = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
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
        transform.position = Vector2.MoveTowards(transform.position,
        waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

        if (transform.position == waypoints[waypointIndex].transform.position)
        {
            waypointIndex += 1;
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }

        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
            transform.localScale = new Vector2(-Mathf.Sign(myRigidbody.velocity.x), 1f);
        }

        else if (healthAmount <= 0)
        {
            childpolygon.enabled = false;
            myboxCollider.enabled = false;
            moveSpeed = 0f;
            animator.SetTrigger("isdogDying");
            
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            moveSpeed = runSpeed;
            
            animator.SetTrigger("isdogRunning");
            myboxCollider.enabled = false;
            AfterPlayerDies();
        }
    }

    void AfterPlayerDies()
    {
        if (childpolygon.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            moveSpeed = 0f;
            animator.SetTrigger("isdogBites");
        }
    }

   
}   