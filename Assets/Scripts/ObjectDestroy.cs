using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroy : MonoBehaviour
{

    HealthEnemy health;
    [SerializeField] float timebeforeDestroy = 2f;

    void Start()
    {
        health = GetComponent<HealthEnemy>();
    }

    void Update()
    {
        PrepToDestroyObject();
    }

    void PrepToDestroyObject()
    {
        int healthAmount = health.GetHealth();

        if (healthAmount < 0)
        {
            Invoke("DestroyObject", timebeforeDestroy);
        }
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
