using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hunter : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;
    public GameObject prey;
    public GameObject nest;

    Vector3 direction;

    float hungerVal;
    public float speed, rotSpeed;
    public float sleepStart;
    public float sleepEnd;
    public float maxHunger;
    public float attackDistance;
    public float damage;
    public float healWithBite;
    [Range(10f,90f)]
    public float whenIAmHungry = 50f;
    List<GameObject> preys;

    //public float wanderRadius, wanderDistance, wanderJitter;
    

    private void OnEnable()
    {
        //TimeManager.OnMinuteChanged += TimeCheck;
        TimeManager.OnHourChanged += TimeCheck;
    }

    private void OnDisable()
    {
        //TimeManager.OnMinuteChanged -= TimeCheck;
        TimeManager.OnHourChanged -= TimeCheck;
    }
    // Start is called before the first frame update
    void Start()
    {
        getListsOfWorldManager();

        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        maxHunger = GetComponent<HungerAllg>().maxHunger;
        agent.stoppingDistance = attackDistance;

        /*
        preys = WorldManager.preys;
        
        preys = new List<GameObject>();
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Prey")) 
        {
            preys.Add(p);
        }
        */
    }


    // Update is called once per frame
    void Update()
    {
        getListsOfWorldManager();

        hungerVal = GetComponent<HungerAllg>().hunger;
        //anim.SetFloat("hunger", hungerVal);
        hungerCheck();

         if (preys.Count==0)
        {
            anim.SetFloat("distance", 100);
        }

        if (preys.Count > 0)
        {
            closestPrey();
            
            if(prey != null)
            {
                direction = prey.transform.position - this.transform.position;
                Debug.DrawRay(this.transform.position, direction, Color.green);
            }
        }
    }

    void closestPrey()
    {
        float distance = Mathf.Infinity;

            for (int i = 0; i < preys.Count; i++)
            {
            GameObject p = preys[i];

                if (p == null)
                {            
                    preys.Remove(p);
                }
                else if (Vector3.Distance(transform.position, p.transform.position) < distance)
                {
                    prey = p;
                    distance = Vector3.Distance(transform.position, p.transform.position);
                }
            }
            anim.SetFloat("distance", distance);

        if(distance <= attackDistance)
        {
            anim.SetBool("attackDistance", true);
        }
        else
        {
            anim.SetBool("attackDistance", false);
        }
    }

    void makeDamage()
    {
       //damage = this.damage;
       prey.GetComponent<HungerAllg>().getdamage(damage);
    }

    void hungerCheck()
    {
        if (hungerVal < maxHunger * (whenIAmHungry / 100))
        {
            anim.SetBool("isHungry", true);
            //Debug.Log("Hungrig");
        }
        else
        {
            anim.SetBool("isHungry", false);
            //Debug.Log("Nicht Hungrig");
        }
    }
    private void TimeCheck()
    {
        if (TimeManager.Hour == sleepStart)
        {
            anim.SetBool("isTired", true);
        }
        if(TimeManager.Hour == sleepEnd)
        {
            anim.SetBool("isTired", false);
        }
    }

    private void getListsOfWorldManager()
    {
        string name = gameObject.name;
        switch (name)
        {
            case "Wolf(Clone)":
                preys = WorldManager.sheeps;
                nest = GameObject.Find("WolfNest");
                break;
            case "Fox(Clone)":
                preys = WorldManager.ducks;
                nest = GameObject.Find("FoxNest");
                break;
            default:
                Debug.Log("wrong name:" + name);
                break;
        }
    }
}
       