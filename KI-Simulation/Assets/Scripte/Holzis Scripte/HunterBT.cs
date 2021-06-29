using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Panda;

//https://gist.github.com/IJEMIN/f2510a85b1aaf3517da1af7a6f9f1ed3

public class HunterBT : MonoBehaviour
{
    NavMeshAgent agentBT;
    public GameObject target;
    public List<GameObject> preys;
    Animator anim;

    public float maxWanderDistance;
    public float accurracy;
    public float visibleRange = 20;
    public float damage;
    public float healWithBite;

    [Range(10f, 90f)]
    public float whenIAmHungry = 50f;

    float distance;

    float hunger;
    //bool isHungry = false;


    private void Start()
    {
        agentBT = gameObject.GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        preys = new List<GameObject>();
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Prey"))
        {
            preys.Add(p);
        }
    }

    private void Update()
    {
        hunger = GetComponent<HungerAllg>().hunger;
        distanceToTarget();

        if(target == null)
        {
            anim.SetBool("inAttackRange", false);
        }
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
        if (Vector3.Distance(hit.position, transform.position) <= accurracy)
        {
            Task.current.Succeed();
            Debug.Log("Fertig");
        }
    }

    [Task]
    public bool hungerCheck()
    {
        if (this.hunger < this.GetComponent<HungerAllg>().maxHunger * (whenIAmHungry / 100))
        {
            return true;
        }
        else
        {
           return false;
        }
    }

    [Task]
    public bool checkDistanceToTarget()
    {
        bool isInRange;
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
        if(target != null)
        {
            agentBT.SetDestination(target.transform.position);

            if (agentBT.remainingDistance <= agentBT.stoppingDistance && !agentBT.pathPending)
            {
                Task.current.Succeed();
            }
        }
  
    }

    [Task]
    public void Attack()
    {
     
            if (Vector3.Distance(transform.position, target.transform.position) <= agentBT.stoppingDistance)
            {
                anim.SetBool("inAttackRange", true);
            Task.current.Succeed();
        }
            else
            {
                anim.SetBool("inAttackRange", false);
            Task.current.Fail();
            } 
    }

    public void makeDamage()
    {
        target.GetComponent<HungerAllg>().getdamage(damage);
        GetComponent<HungerAllg>().eating(healWithBite);
    }

    public void distanceToTarget()
    {
      distance = Mathf.Infinity;
        for (int i = 0; i<preys.Count; i++)
        {
                GameObject p = preys[i];

            if(p == null)
            {
                preys.Remove(p);
            }

            if (Vector3.Distance(transform.position, p.transform.position) < distance)
            {
               target = p;
               distance = Vector3.Distance(transform.position, p.transform.position);
            }
        }
        if (target != null)
        {
            Vector3 direction = target.transform.position - this.transform.position;
            Debug.DrawRay(this.transform.position, direction, Color.green);
        }
    }
}

