using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTriggerAnimator : MonoBehaviour
{
    [SerializeField] Animator animator = null;
    [SerializeField] string actionParameter = null;
    [SerializeField] int numberofhacksneeded = 3;
    GameSession session;

    void Start()
    {
        session = GetComponent<GameSession>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        int getTheScore = session.GetScore();

        if (other.CompareTag("Player") && getTheScore == numberofhacksneeded)
             
        {
            animator.SetTrigger(actionParameter);
        }
    }
}
