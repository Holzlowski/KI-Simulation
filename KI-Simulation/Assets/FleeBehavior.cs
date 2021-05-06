using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleeBehavior : StateMachineBehaviour
{

    private Transform hunter;
    private NavMeshAgent agent;
    public float DistanceFlee;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hunter = GameObject.FindGameObjectWithTag("Hunter").transform;
        agent = animator.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       float distance = Vector3.Distance(agent.transform.position, hunter.transform.position);
       if (distance < DistanceFlee)
        {
            Vector3 moveAway = agent.transform.position - hunter.transform.position;
            Vector3 newPos = agent.transform.position + moveAway;

            agent.SetDestination(newPos);
        } else {
            animator.SetBool("isFleeing", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

}
