using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderPrey : StateMachineBehaviour
{

    NavMeshAgent agent;
    public float distanceView;
    PreyAnim prey;
    GameObject[] plants;

    float wanderRadius = 5;
    float wanderDistance = 5;
    float wanderJitter = 10;
    Vector3 wanderTarget;

    bool isHungry;
    public bool cooldownBool;
    public float cooldown;
    private GameObject target;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent = animator.GetComponent<NavMeshAgent>();
       prey = animator.GetComponent<PreyAnim>();
       wanderTarget = Vector3.zero;

       distanceView = prey.distanceView;
       plants = WorldManager.plants;

       cooldown = 0;
       cooldownBool = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        isHungry = animator.GetBool("isHungry");
        if (isHungry) 
        {
            foreach(GameObject plant in plants)
            {
                if(Vector3.Distance(agent.transform.position, plant.transform.position) <= distanceView)
                {
                    animator.GetComponent<PreyAnim>().target = plant.transform;
                    animator.SetBool("hasTarget", true);
                    animator.SetBool("isWander", false);
                }
            }
        }

        if(cooldownBool)
        {
            cooldown -= 1f * Time.deltaTime;
            if(cooldown <= 0)
            {
                cooldownBool = false;
                cooldown = 0;
            }
        } else {
            wander();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    private void wander()
    {
        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0, Random.Range(-1.0f, 1.0f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = agent.gameObject.transform.InverseTransformVector(targetLocal);

        agent.SetDestination(targetWorld);
        cooldown = 10;
        cooldownBool = true;
    }
}
