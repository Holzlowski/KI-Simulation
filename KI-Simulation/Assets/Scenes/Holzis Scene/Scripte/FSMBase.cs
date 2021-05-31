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

    public float speed;
    public float rotSpeed;
    public float acceleration;
    public float damage;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hunter = animator.gameObject;
        agent = hunter.GetComponent<NavMeshAgent>();
        prey = hunter.GetComponent<Hunter>().prey;

        agent.speed = speed;
        agent.angularSpeed = rotSpeed;
        agent.acceleration = acceleration;
    }

}
