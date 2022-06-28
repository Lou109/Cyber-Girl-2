using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Animator animator = null;
    [SerializeField] string triggerParameter = null;
    [SerializeField] ParticleSystem impactEffect = null;
    [SerializeField] GameObject firepoint;
    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth = 50;
    public HealthBar healthBar;


    void Start()
    {
        impactEffect.GetComponent<ParticleSystem>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayImpactEffect();
            damageDealer.Hit();
        }
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.GetHealth(currentHealth);

        if (currentHealth < 0)
        {
            animator.SetTrigger(triggerParameter);
        }
    }

    void PlayImpactEffect()
    {
        if (impactEffect != null)
        {
            ParticleSystem instance = Instantiate(impactEffect, firepoint.transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }
}

