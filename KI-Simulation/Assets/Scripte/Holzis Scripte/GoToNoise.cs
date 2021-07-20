using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToNoise : FSMBase
{
    Vector3 noisePosition;
    float originalAttackDistance;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        agent.speed = originalSpeed+1;
        agent.stoppingDistance = 0.2f;
        noisePosition = animator.GetComponent<Hunter>().noisePosition;
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(noisePosition);
        if (Vector3.Distance(animator.transform.position, noisePosition) <= 1f) 
        {
            animator.SetBool("lookingAround", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.stoppingDistance = originalAttackDistance;
        animator.SetBool("lookingAround", false);
    }
}
