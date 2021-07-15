using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSMBase : StateMachineBehaviour
{
    [HideInInspector]
    public GameObject hunter;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public GameObject prey;
    [HideInInspector]
    public Vector3 wanderTarget = Vector3.zero;
    [HideInInspector]
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
     
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        prey = hunter.GetComponent<Hunter>().prey;
    }

}
