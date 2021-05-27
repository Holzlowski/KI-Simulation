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

    public float attackSpeed;
    public float speed = 2f;
    public float rotSpeed = 2f;
    public float accuracy = 2f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hunter = animator.gameObject;
        agent = hunter.GetComponent<NavMeshAgent>();
        prey = GameObject.FindGameObjectWithTag("Prey");
    }

}
