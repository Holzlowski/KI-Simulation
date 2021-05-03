using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*Quellen:
https://www.youtube.com/watch?v=UjkSFoLxesw&t=137s
*/

public class Flee : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    public GameObject hunter;

    //public LayerMask whatIsGround;

    //Fleeing
    public float DistanceFlee = 5f;
    bool isFleeing;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Start is called before the first frame update
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Fleeing();

        if (isFleeing == false) Patroling();
        Debug.Log(isFleeing);

        
       
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            navMeshAgent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }

    }

    private void Fleeing()
    {
        float distance = Vector3.Distance(transform.position, hunter.transform.position);
        Debug.Log("Distance: " + distance);

        // Flee from Target
        if (distance < DistanceFlee)
        {
            isFleeing = true;
            Vector3 moveAway = transform.position - hunter.transform.position;
            Vector3 newPos = transform.position + moveAway;

            navMeshAgent.SetDestination(newPos);
        }
        else
        {
            isFleeing = false;
        }
    }

    private void SearchWalkPoint()
    {
        //Calculate random Point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f))
        {
            walkPointSet = true;
        }
    }

}
