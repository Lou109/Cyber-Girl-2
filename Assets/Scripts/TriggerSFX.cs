using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSFX : MonoBehaviour
{
    [SerializeField] AudioSource playSound;
    BoxCollider2D myboxCollider;

    void Start()
    {
        myboxCollider = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        playSound.Play();
        myboxCollider.enabled = false;
    }
}
