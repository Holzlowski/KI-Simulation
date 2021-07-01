using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hide : StateMachineBehaviour
{
    private NavMeshAgent agent;
    public float fleeSpeed;
    private float normalSpeed;
    Prey prey;
    public GameObject theHunter;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        theHunter = animator.GetComponent<Prey>().theHunter;

        normalSpeed = agent.speed;
        agent.speed = fleeSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 hideSpot = animator.GetComponent<Prey>().chosenSpot;
        animator.GetComponent<Prey>().hide();

        Debug.DrawLine(animator.transform.position, hideSpot, Color.red);
        Debug.Log("Ich verstecke mich");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
