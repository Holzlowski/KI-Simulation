using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkingBehavior : StateMachineBehaviour
{
    private bool hungry;
    private bool tired;
    private Transform target;
    private GameObject nest;

    NavMeshAgent agent;
    Prey prey;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        prey = animator.GetComponent<Prey>();

        hungry = animator.GetComponent<Prey>().hungry;
        tired = animator.GetComponent<Prey>().tired;
        target = animator.GetComponent<Prey>().target;
        nest = animator.GetComponent<Prey>().nest;
        agent.SetDestination(target.position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       float distanceToTarget = Vector3.Distance(agent.transform.position, target.position);

        if(distanceToTarget <= agent.stoppingDistance)
        {
            if(target == nest.transform)
            {
                animator.SetBool("isSleeping", true);
                animator.SetBool("hasTarget", false);
            } else if (hungry) 
            {
                animator.SetBool("isEating", true);
                animator.SetBool("hasTarget", false);
            }
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}
