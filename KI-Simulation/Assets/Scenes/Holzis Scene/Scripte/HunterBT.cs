using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Panda;

//https://gist.github.com/IJEMIN/f2510a85b1aaf3517da1af7a6f9f1ed3

public class HunterBT : MonoBehaviour
{
    NavMeshAgent agentBT;
    GameObject target;
    List<GameObject> preys;

    public float maxWanderDistance;
    public float accurracy;

    float hunger;
    bool isHungry = false;


    private void Start()
    {
        agentBT = gameObject.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        hunger = GetComponent<HungerAllg>().hunger;
        //preys = WorldManager.preys;
    }

    [Task]
   public void wanderBT()
    {
        // Get Random Point inside Sphere which position is transform.position, radius is maxWanderDistance
        Vector3 randomPos = Random.insideUnitSphere * maxWanderDistance + transform.position;
        // NavMesh Sampling Info Container
        NavMeshHit hit;
        // from randomPos find a nearest point on NavMesh surface in range of maxDistance
        NavMesh.SamplePosition(randomPos, out hit, maxWanderDistance, NavMesh.AllAreas);

        agentBT.SetDestination(hit.position);
        if(Vector3.Distance(hit.position, transform.position) <= accurracy)
        {
            Task.current.Succeed();
        }  
    }

    [Task]
    public bool hungerCheck(float percent)
    {
        if (this.hunger < this.GetComponent<HungerAllg>().maxHunger * percent)
        {
            return true;
        }
        else
        {
            return false;
        }
        //
    }

    [Task]
    public bool checkDistanceToTarget(float visibleRange)
    {
        float distance = Mathf.Infinity;
        bool isInRange = false;

        for (int i = 0; i < preys.Count; i++)
        {
            GameObject p = preys[i];

            if (Vector3.Distance(transform.position, p.transform.position) < distance)
            {
                target = p;
                distance = Vector3.Distance(transform.position, p.transform.position);
            }
        }
        if (distance <= visibleRange)
        {
            isInRange = true;
        }
        else
        {
            isInRange = false;
        }

        return isInRange;
    }

    [Task]
    public void moveToTarget()
    {
        agentBT.SetDestination(target.transform.position);
    }
}

