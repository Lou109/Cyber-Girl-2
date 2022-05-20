using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPickup : MonoBehaviour
{

    [SerializeField]int pointsForComputerPickup = 1;
    bool wasCollected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(pointsForComputerPickup);
        }
    }
}
