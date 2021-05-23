using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EatingPrey : StateMachineBehaviour
{
    bool hungry;
    NavMeshAgent agent;
    private float normalSpeed;
    PreyAnim prey;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        prey = animator.GetComponent<PreyAnim>();
        normalSpeed = agent.speed;
        //agent.speed = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hungry = animator.GetBool("isHungry");
        if(hungry)
        {
            animator.SetTrigger("Eat");
        } else {
            animator.SetBool("isEating", false);
            prey.setTarget(null);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //agent.speed = normalSpeed;
        animator.ResetTrigger("Eat");
    }
}
