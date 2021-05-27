using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSMBase : StateMachineBehaviour
{
    public GameObject NPC;
    public NavMeshAgent agent;
    public GameObject prey;
    public float speed = 2f;
    public float rotSpeed = 2f;
    public float accuracy = 2f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        agent = NPC.GetComponent<NavMeshAgent>();
    }

}
