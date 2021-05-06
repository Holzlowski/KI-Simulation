using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolBehaviorPrey : StateMachineBehaviour
{

    private NavMeshAgent agent;
    private Transform destination;
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public float y;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        destination = GameObject.Find("PatrolDestination").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        destination.position = new Vector3(Random.Range(minX, maxX), y, Random.Range(minZ, maxZ));
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(destination.position);
        if(Vector3.Distance(destination.position, agent.transform.position) < 0.2f)
        {
            destination.position = new Vector3(Random.Range(minX, maxX), y, Random.Range(minZ, maxZ));
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}