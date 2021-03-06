using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EatingPrey : StateMachineBehaviour
{
    bool hungry;
    NavMeshAgent agent;
    Prey prey;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        prey = animator.GetComponent<Prey>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hungry = animator.GetComponent<Prey>().hungry;
        if(hungry && animator.GetComponent<Prey>().target != null)
        {
            animator.SetTrigger("Eat");
        } else {
            animator.GetComponent<Prey>().target = null;
            animator.SetBool("isEating", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Eat");
    }
}
