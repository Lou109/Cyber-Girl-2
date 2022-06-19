using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lineOfSight;
    [SerializeField] CircleCollider2D mycircleCollider;
   
    Transform player;
    Health health;
    
   
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        health = GetComponent<Health>();
        mycircleCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        int healthAmount = health.GetHealth();

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSight )
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        if (healthAmount <= 0)
        {
            speed = 0;
            mycircleCollider.enabled = false;
            
        } 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
       
    }

}