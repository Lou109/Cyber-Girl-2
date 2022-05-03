using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTriggerAnimator : MonoBehaviour
{
    [SerializeField] Animator animator = null;
    [SerializeField] string actionParameter = null;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))

        {
            animator.SetTrigger(actionParameter);
        }
    }
}
