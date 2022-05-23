using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPickup : MonoBehaviour
{
    [SerializeField] string actionParameter = null;
    [SerializeField]int pointsForComputerPickup = 1;
    bool wasCollected = false;
    [SerializeField]Animator animator;
    [SerializeField] AudioSource playSound;
    [SerializeField] BoxCollider2D myboxCollider;
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(pointsForComputerPickup);
            animator.SetTrigger(actionParameter);
            playSound.Play();
            myboxCollider.enabled = false;
        }
        
    }
}
