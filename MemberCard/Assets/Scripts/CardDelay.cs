using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


public class CardDelay : StateMachineBehaviour
{
    private float timer = 0f;
    private bool triggered = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        triggered = false;
        animator.speed = 0f; 
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        float delay = animator.GetFloat("Delay");

        if(!triggered && timer >= delay)
        {
            animator.speed = 1f;
            triggered = true;
        }
    }
}