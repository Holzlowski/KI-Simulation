using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderPrey : StateMachineBehaviour
{

    NavMeshAgent agent;
    private float distanceView;
    PreyAnim prey;
    List<GameObject> plants;

    float wanderRadius = 10;
    float wanderDistance = 10;
    float wanderJitter = 10;
    Vector3 wanderTarget;

    bool isHungry;
    bool hasTarget = false;
    private GameObject target;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent = animator.GetComponent<NavMeshAgent>();
       prey = animator.GetComponent<PreyAnim>();
       wanderTarget = Vector3.zero;

       distanceView = prey.distanceView;
       plants = prey.getPlants();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        isHungry = animator.GetBool("isHungry");
        if (isHungry && !hasTarget) {
            foreach(GameObject plant in plants)
            if(Vector3.Distance(agent.transform.position, plant.transform.position) <= distanceView)
            {
                prey.setTarget(plant.transform);
                animator.SetBool("isWander", false);
            }
        }
        
        if(!hasTarget)
        {
            wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0, Random.Range(-1.0f, 1.0f) * wanderJitter);
            wanderTarget.Normalize();
            wanderTarget *= wanderRadius;

            Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
            Vector3 targetWorld = agent.gameObject.transform.InverseTransformVector(targetLocal);

            agent.SetDestination(targetWorld);
        } 
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}
