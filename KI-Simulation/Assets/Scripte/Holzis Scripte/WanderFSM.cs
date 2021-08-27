using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderFSM : FSMBase
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        agent.speed = originalSpeed;
        
        //get input from inspector
        wanderRadius = hunter.GetComponent<Hunter>().wanderRadius;
        wanderDistance = hunter.GetComponent<Hunter>().wanderDistance;
        wanderJitter = hunter.GetComponent<Hunter>().wanderJitter;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //a point that moves in the circumference of the circle
        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0, Random.Range(-1.0f, 1.0f) * wanderJitter);
        // Vector gets a length of 1
        wanderTarget.Normalize();
        // the normalized vector is multiplied by the radius to put it directly on the circumference
        wanderTarget *= wanderRadius;

        // the circle is shifted by wanderDistance forward from the Agent
        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        // converts localtarget into worldposition
        Vector3 targetWorld = hunter.transform.InverseTransformVector(targetLocal);

        // moves agent to targetworld position
        agent.SetDestination(targetWorld);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}

/* {
        if (runTimer)
        {
            time += Time.deltaTime;

            if (time > Random.Range(0.3f, 1f))
            {
                runTimer = false;
               

                //a point that moves in the circumference of the circle
                wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0, Random.Range(-1.0f, 1.0f) * wanderJitter);
                // Vector gets a length of 1
                wanderTarget.Normalize();
                // the normalized vector is multiplied by the radius to put it directly on the circumference
                wanderTarget *= wanderRadius;

                // the circle is shifted by wanderDistance forward from the Agent
                Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
                // converts localtarget into worldposition
                Vector3 targetWorld = hunter.transform.TransformVector(targetLocal);

                // moves agent to targetworld position
                agent.SetDestination(targetWorld);

                time = 0f;
            }
        }
        else if(!runTimer)
        {
            runTimer = true;
        }
    }
*/