using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleeBehaviorPrey : StateMachineBehaviour
{

    private List<GameObject> hunters;
    private NavMeshAgent agent;
    public float distanceView;
    public float fleeSpeed;
    private float normalSpeed;
    PreyAnim prey;
    public GameObject theHunter;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();

        distanceView = animator.GetComponent<PreyAnim>().getDistanceView();
        hunters = animator.GetComponent<PreyAnim>().getHunters();
        normalSpeed = agent.speed;
        agent.speed = fleeSpeed;
        theHunter = animator.GetComponent<PreyAnim>().theHunter; 
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (theHunter != null)
        {
            float distance = Vector3.Distance(agent.transform.position, theHunter.transform.position);
            if (distance < distanceView*1.5)
            {
                animator.SetBool("isFleeing", false);
            } else {
            
            Vector3 moveAway = agent.transform.position - theHunter.transform.position;
            Vector3 newPos = agent.transform.position + moveAway;

            agent.SetDestination(newPos);
            }
        } else {
            animator.SetBool("isFleeing", false);
        }  
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent.speed = normalSpeed;
    }

}
