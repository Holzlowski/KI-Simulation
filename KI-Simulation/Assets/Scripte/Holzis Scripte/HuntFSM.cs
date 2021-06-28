using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HuntFSM : FSMBase
{
    float originalSpeed;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        originalSpeed = agent.speed;

        if(animator.GetComponent<Hunter>().huntingSpeed <= 0)
        {
            agent.speed += 5;
        }
        else
        {
            agent.speed = animator.GetComponent<Hunter>().huntingSpeed;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 preyDirection = animator.GetComponent<Hunter>().direction;
        float preySpeed = prey.GetComponent<NavMeshAgent>().speed;

        float lookAhaead = preyDirection.magnitude / (agent.speed + preySpeed);
        Vector3 location = prey.transform.position + prey.transform.forward * lookAhaead;
        agent.SetDestination(location);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.speed = originalSpeed;
    }
}
