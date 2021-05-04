using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent theAgent;
    public float huntRange;

    // Start is called before the first frame update
    void Start()
    {
        theAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Prey").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(theAgent.remainingDistance < huntRange)
        {
            theAgent.SetDestination(target.position);
        }
        
    }
}
