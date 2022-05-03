using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Animator animator = null;
    [SerializeField] string triggerParameter = null;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        PlayAnimation();
    }

    void PlayAnimation()
    {
        animator.SetTrigger(triggerParameter);
    }

}
