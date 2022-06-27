using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lineOfSight;
    [SerializeField] CircleCollider2D mycircleCollider;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioSource myAudioSource;
    bool playExplosionSFX;
   
    Transform player;
    HealthEnemy healthEnemy;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        healthEnemy = GetComponent<HealthEnemy>();
        mycircleCollider = GetComponent<CircleCollider2D>();
       
    }

    void Update()
    {
        int healthAmount = healthEnemy.GetHealth();

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSight )
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        if (healthAmount <= 0 && !playExplosionSFX)
        {
            mycircleCollider.enabled = false;
            speed = 0;
            myAudioSource.PlayOneShot(explosionSound);
            playExplosionSFX = true;

        } 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }
}