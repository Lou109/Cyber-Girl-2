using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float firingRate = 0.2f;
    [SerializeField] Transform gun;
    [SerializeField] AudioSource playerShootingAudio;
  
    
    PlayerHealth playerHealth;
    public bool isFiring;
    Coroutine firingCoroutine;
    float xSpeed;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
      
    }

    void Update()
    {
        Fire();
        StopFireIfDead();
    }

    void Fire()
    {

        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }

        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    public void StopFireIfDead()
    {
        int healthAmount = playerHealth.GetHealth();

        if (healthAmount <= 0)
        {
            isFiring = false;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject instance = Instantiate(projectilePrefab,
                                                  gun.position,
                                            transform.rotation);

            xSpeed = transform.localScale.x * bulletSpeed;
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();

            if (rb != null)

            {
                rb.velocity = new Vector2(xSpeed, 0);
                playerShootingAudio.Play();
            }
            Destroy(instance, projectileLifetime);
            

          
            
            yield return new WaitForSeconds(firingRate);
        }     
    }
}