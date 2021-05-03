using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAgent : MonoBehaviour
{
    public GameObject characterDestination;
    NavMeshAgent theAgent;
    private Animator animator;
    private bool walk = false;

    // Start is called before the first frame update
    void Start()
    {
        theAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        theAgent.SetDestination(characterDestination.transform.position);
        animator.SetBool("walk", true);
    }
}
