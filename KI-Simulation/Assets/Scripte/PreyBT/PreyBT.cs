using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using UnityEngine.AI;

public class PreyBT : MonoBehaviour
{
    public float distanceView;
    //FOR WANDER
    public float wanderDistance;
    Vector3 wanderTarget;
    
    List<GameObject> plants;
    NavMeshAgent agent;
    GameObject target;

    float hunger;

    // Start is called before the first frame update
    void Start()
    {
        plants = new List<GameObject>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        foreach(GameObject plant in GameObject.FindGameObjectsWithTag("Plant"))
        {
            plants.Add(plant);
        }
    }

    // Update is called once per frame
    void Update()
    {
        hunger = GetComponent<HungerAllg>().hunger;
    }

    [Task]
    public void wander()
    {
        wanderTarget += new Vector3(Random.Range(-wanderDistance, wanderDistance), 0, Random.Range(-wanderDistance, wanderDistance));
        NavMeshHit hit;
            if (NavMesh.SamplePosition(wanderTarget, out hit, 1f, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
                Task.current.Succeed();
            }
    }

    [Task]
    public void moveToDestination()
    {
        if(agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
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
    }

    [Task]
    public void searchFood()
    {
        foreach(GameObject plant in plants)
        {
            if(Vector3.Distance(agent.transform.position, plant.transform.position) <= distanceView)
            {
                agent.SetDestination(plant.transform.position);
                Task.current.Succeed();
            }
        }

        if(agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {  
            Task.current.Fail();
        }
    }

}
