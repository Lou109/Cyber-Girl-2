using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] Animator animator = null;
    [SerializeField] string triggerParameter = null;
    [SerializeField] ParticleSystem impactEffect = null;
    [SerializeField] GameObject firepoint;
    [SerializeField] int health = 50;

    void Start()
    {
        impactEffect.GetComponent<ParticleSystem>();
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
        return health;
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
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
