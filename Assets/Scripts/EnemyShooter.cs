using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float firingRate = 0.2f;
    [SerializeField] Transform gun;
    [SerializeField] AudioSource enemyShootingAudio;
    [SerializeField] float shootingRange = 4f;
   
    Transform player;
    Health health;
    bool enemyisFiring = true;
    Coroutine firingCoroutine;
    float xSpeed;

    void Start()
    {
        health = GetComponent<Health>();
        player = GameObject.FindGameObjectWithTag("Player").transform;  
    }

    void Update()
    {
       
        EnemyFire();
    }
    
    void EnemyFire()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
    
            if (distanceFromPlayer <= shootingRange && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
         
            if (distanceFromPlayer >= shootingRange && firingCoroutine != null)
        {

            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
            enemyisFiring = false;
        }               
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            int healthAmount = health.GetHealth();

            if (healthAmount >= 0)
            {
  
                GameObject instance = Instantiate(projectilePrefab,
                                                 gun.position,
                                           transform.rotation);

                Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
                xSpeed = transform.localScale.x * bulletSpeed;
                enemyShootingAudio.Play();
                rb.velocity = new Vector2(xSpeed, 0);
                Destroy(instance, projectileLifetime);
            }

            yield return new WaitForSeconds(firingRate);

        }
    }
        void OnDrawGizmosSelected()
        {
       
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, shootingRange);    
    }
}

