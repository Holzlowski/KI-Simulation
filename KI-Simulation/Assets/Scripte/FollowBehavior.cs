using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowBehavior : StateMachineBehaviour
{
    private Transform target;
    private NavMeshAgent agent;
    private float huntRange;
    HunterAnim hunter;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hunter = animator.GetComponent<HunterAnim>();
        huntRange = hunter.getHuntRange();
        agent = animator.GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Prey").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector3.Distance(agent.transform.position, target.position); 
       if(distance < huntRange)
        {
            agent.SetDestination(target.position);
        } else {
            animator.SetBool("isFollowing", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}
