using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderFSM : FSMBase
{
    //public float wanderRadius = 5f, wanderDistance = 30f, wanderJitter = 1f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        agent.speed = originalSpeed;

        if(animator.GetBool("lookingAround") == true)
        {
            wanderJitter = 4f;
            wanderDistance = 0F;
            wanderRadius = 4f;
        }
        else
        {
            wanderRadius = hunter.GetComponent<Hunter>().wanderRadius;
            wanderDistance = hunter.GetComponent<Hunter>().wanderDistance;
            wanderJitter = hunter.GetComponent<Hunter>().wanderJitter;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0, Random.Range(-1.0f, 1.0f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = hunter.transform.InverseTransformVector(targetLocal);

        agent.SetDestination(targetWorld);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
