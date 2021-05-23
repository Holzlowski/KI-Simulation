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

    private bool hasToFlee;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hunters = new List<GameObject>();
        foreach(GameObject hunter in GameObject.FindGameObjectsWithTag("Hunter"))
        {
            hunters.Add(hunter);
        }

        agent = animator.GetComponent<NavMeshAgent>();
        prey = animator.GetComponent<PreyAnim>();

        distanceView = prey.getDistanceView();
        normalSpeed = agent.speed;
        agent.speed = fleeSpeed;
        hasToFlee = false;
       
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        foreach(GameObject hunter in hunters)
        {
            float distance = Vector3.Distance(agent.transform.position, hunter.transform.position);
            if (distance < distanceView*1.5)
            {
                Vector3 moveAway = agent.transform.position - hunter.transform.position;
                Vector3 newPos = agent.transform.position + moveAway;

                agent.SetDestination(newPos);
                hasToFlee = true;
            }
        }
        if(!hasToFlee)
        {
            animator.SetBool("isFleeing", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent.speed = normalSpeed;
    }

}
