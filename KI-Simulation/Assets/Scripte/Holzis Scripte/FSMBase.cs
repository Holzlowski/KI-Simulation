using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSMBase : StateMachineBehaviour
{
    public GameObject hunter;
    public NavMeshAgent agent;
    public GameObject prey;
    public Vector3 wanderTarget = Vector3.zero;

    public float originalSpeed;

    [HideInInspector]
    public float wanderRadius, wanderDistance, wanderJitter;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hunter = animator.gameObject;
        agent = hunter.GetComponent<NavMeshAgent>();
        prey = hunter.GetComponent<Hunter>().prey;

        agent.speed = hunter.GetComponent<Hunter>().speed;
        originalSpeed = hunter.GetComponent<Hunter>().orginalSpeed;
        agent.angularSpeed = hunter.GetComponent<Hunter>().rotSpeed;
        agent.acceleration = hunter.GetComponent<Hunter>().acceleration;
     
        this.wanderRadius = hunter.GetComponent<Hunter>().wanderRadius;
        this.wanderDistance = hunter.GetComponent<Hunter>().wanderDistance;
        this.wanderJitter = hunter.GetComponent<Hunter>().wanderJitter;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        prey = hunter.GetComponent<Hunter>().prey;
    }

}
