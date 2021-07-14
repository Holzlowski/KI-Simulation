using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;
using UnityEngine.AI;

public class PreyBT : MonoBehaviour
{
    public float distanceView;
    [Range(0, 1)] 
    public float notHungryAnymore;
    [Range(0, 1)] 
    public float isHungryFloat;
    //FOR WANDER
    public float wanderDistance;
    Vector3 wanderTarget;
    
    List<GameObject> plants;
    NavMeshAgent agent;
    GameObject target;

    float hunger;
    bool isHungryBool;
    float maxHunger;

    // Start is called before the first frame update
    void Start()
    {
        plants = new List<GameObject>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        foreach(GameObject plant in GameObject.FindGameObjectsWithTag("Plant"))
        {
            plants.Add(plant);
        }
        maxHunger = GetComponent<HungerAllg>().maxHunger;
        //isHungryBool = false;
    }

    // Update is called once per frame
    void Update()
    {
        hunger = GetComponent<HungerAllg>().hunger;
        if(!isHungryBool && hunger < maxHunger * isHungryFloat){
            isHungryBool = true;
            //Debug.Log("hungrig");
        }
        if(isHungryBool && hunger > maxHunger * notHungryAnymore)
        {
            isHungryBool = false;
            //Debug.Log("nicht mehr hungrig");
        }
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
        } else {
            Task.current.Fail();
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
    public bool hungerCheck()
    {
        if (this.hunger < maxHunger * notHungryAnymore)
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
                target = plant;
                agent.SetDestination(plant.transform.position);
                Task.current.Succeed();
            }
        }

        if(agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {  
            Task.current.Fail();
        }
    }

    [Task]
    public void eat(int value)
    {
        if(target != null)
        {
            target.GetComponent<Plant>().losingCapacity(value);
            GetComponent<HungerAllg>().eating(value);

        } else {
            target = null;
            Task.current.Fail();
        }
        target = null;
        Task.current.Succeed();
    }

    [Task]
    public bool isHungry()
    {
        return isHungryBool;
    }

}
