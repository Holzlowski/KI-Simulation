using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookingAround : FSMBase
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        animator.GetComponent<Hunter>().searchingPosition = animator.transform.position;
        animator.GetComponent<Hunter>().StartCoroutine("lookingAround");
        animator.GetComponent<Hunter>().StartCoroutine("stopLookingAround");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(animator.GetComponent<Hunter>().randomPoint);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Hunter>().StopCoroutine("lookingAround");
        animator.GetComponent<Hunter>().StopCoroutine("stopLookingAround");
        animator.SetBool("stopLookingAround", false);
    }
}
