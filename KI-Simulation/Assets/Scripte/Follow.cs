using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent theAgent;
    public float huntRange;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        theAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        
        target = GameObject.FindGameObjectWithTag("Prey").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(theAgent.remainingDistance < huntRange)
        {
            anim.SetBool("isFollowing", true);
            theAgent.SetDestination(target.position);
        } else {
            anim.SetBool("isFollowing", false);
        }
        
    }
}
