using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : NPCBaseFSM
{
    GameObject[] wayPoints;
    int currentWP;

    private void Awake()
    {
        wayPoints = GameObject.FindGameObjectsWithTag("waypoint");
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        currentWP = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (wayPoints.Length == 0) return;
        if(Vector3.Distance(wayPoints[currentWP].transform.position, NPC.transform.position) < accuracy)
        {
            currentWP++;
            if(currentWP >= wayPoints.Length)
            {
                currentWP = 0;
            }
        }

        //rotate towards target
        Vector3 lookAtGoal = new Vector3(wayPoints[currentWP].transform.position.x, NPC.transform.position.y, wayPoints[currentWP].transform.position.z);
        var direction = lookAtGoal - NPC.transform.position;
        NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
        NPC.transform.Translate(0, 0, Time.deltaTime * speed);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
