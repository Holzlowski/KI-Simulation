using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderPrey : StateMachineBehaviour
{

    NavMeshAgent agent;
    public float distanceView;
    Prey prey;
    List<GameObject> plants;

    float wanderRadius;
    float wanderDistance;
    float wanderJitter;
    Vector3 wanderTarget;

    bool hungry;
    private GameObject target;

    bool isDirSafe = false;
    float vRotation = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        prey = animator.GetComponent<Prey>();
        wanderRadius = animator.GetComponent<Prey>().wanderRadius;
        wanderDistance = animator.GetComponent<Prey>().wanderDistance;
        wanderJitter = animator.GetComponent<Prey>().wanderJitter;
        //wanderTarget = Vector3.zero;

        distanceView = prey.distanceView;
        plants = animator.GetComponent<Prey>().plants;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        hungry = prey.hungry; 
        if (hungry) 
        {
            foreach(GameObject plant in plants)
            {
                if(Vector3.Distance(agent.transform.position, plant.transform.position) <= distanceView)
                {
                    animator.GetComponent<Prey>().target = plant;
                    animator.SetBool("hasTarget", true);
                    animator.SetBool("isWander", false);
                }
            }
        }
        wander();
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
        Vector3 newPos = agent.gameObject.transform.InverseTransformVector(targetLocal);
        agent.SetDestination(newPos);

        /*while (!isDirSafe)
        {
            //Rotate the direction of the Enemy to move
            newPos = Quaternion.Euler(0, vRotation, 0) * newPos;

            LayerMask mask = LayerMask.GetMask("Obstacle");
            //Shoot a Raycast out to the new direction with 5f length (as example raycast length) and see if it hits an obstacle
            bool isHit = Physics.Raycast(agent.transform.position, newPos, out RaycastHit hit, 3f, mask);

            if (hit.transform == null)
            {
                //If the Raycast to the flee direction doesn't hit a wall then the Enemy is good to go to this direction
                agent.SetDestination(newPos);
                isDirSafe = true;
            }

            //Change the direction of fleeing is it hits a wall by 20 degrees
            if (isHit && hit.transform.CompareTag("Obstacle"))
            {
                vRotation += 20;
                isDirSafe = false;
            }
            else
            {
                //If the Raycast to the flee direction doesn't hit a wall then the Enemy is good to go to this direction
                agent.SetDestination(newPos);
                isDirSafe = true;
            }
        }*/
    }  
}