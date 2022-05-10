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
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;
    [SerializeField] bool useAI;
   
    Health health;
    public bool isFiring;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;
  

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    float xSpeed;

    void Start()
    {
        health = GetComponent<Health>();

        if (useAI)
        {
            isFiring = true;
        }
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
        int healthAmount = health.GetHealth();

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

            if (rb != null && !useAI)

            {
                audioPlayer.PlayShootingClip();
            }
    
            float timeToNextProjectile = Random.Range(firingRate - firingRateVariance, firingRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);
            rb.velocity = new Vector2(xSpeed, 0);
            Destroy(instance, projectileLifetime);
            yield return new WaitForSeconds(timeToNextProjectile);
        }
      
    }
}