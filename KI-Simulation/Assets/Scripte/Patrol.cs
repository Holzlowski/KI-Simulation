using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public Transform destination;
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public float y;

    NavMeshAgent theAgent;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        theAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        destination.position = new Vector3(Random.Range(minX, maxX), y, Random.Range(minZ, maxZ));
        anim.SetBool("isPatroling", true);
    }

    // Update is called once per frame
    void Update()
    {
        theAgent.SetDestination(destination.position);
        if(Vector3.Distance(destination.position, theAgent.transform.position) < 0.2f)
        {
            destination.position = new Vector3(Random.Range(minX, maxX), y, Random.Range(minZ, maxZ));
        }
    }
}
