using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hunter : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;

    [HideInInspector]
    public GameObject prey;
    [HideInInspector]
    public GameObject nest;
    List<GameObject> preys;
    public LayerMask layer;
    
    [HideInInspector]
    public Vector3 direction;
    [HideInInspector]
    public float distanceToPrey;

    [HideInInspector]
    public float orginalSpeed;

    bool canSee, canSmell, canHear, canSense;

    [Header("Hunger Settings")]
    public float maxHunger;
    [Range(10f, 90f)]
    public float whenIAmHungry = 50f;
    float hungerVal;

    [Header("Movement Settings")]
    public float speed;
    public float huntingSpeed;
    public float rotSpeed;
    public float acceleration;
    public float wanderDistance, wanderRadius, wanderJitter;

    [Header("Perception Settings")]
    public float visibleDistance = 20f;
    public float visibleAngle = 60f;
    public float smellDistance = 20f;
    public float hearDistance = 40f;
    [HideInInspector]
    public Vector3 noisePosition;

    [Header("Sleep Settings")]
    public float sleepStart;
    public float sleepEnd;

    [Header("Attack and Healing Settings")]
    public float attackDistance;
    public float damage;
    public float healWithBite;

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
        agent.speed = this.speed;
        orginalSpeed = agent.speed;
    }


    // Update is called once per frame
    void Update()
    {
        getListsOfWorldManager();
        hungerCheck();
        closestPrey();
        if(prey != null)
        {
            direction = prey.transform.position - this.transform.position;
            canSeePreyCheck();
            canSmellPreyCheck(distanceToPrey);
            canHearCheck(distanceToPrey);
            canSensePrey();

            if (canSee == true)
            {
                Debug.DrawRay(this.transform.position, direction, Color.green);
            }
        }  
    }

    void closestPrey()
    {
        if(preys.Count > 0)
        {
            float distanceToPrey = Mathf.Infinity;

            for (int i = 0; i < preys.Count; i++)
            {
                GameObject p = preys[i];

                if (p == null)
                {
                    preys.Remove(p);
                }
                else if (Vector3.Distance(transform.position, p.transform.position) < distanceToPrey)
                {
                    prey = p;
                    distanceToPrey = Vector3.Distance(transform.position, p.transform.position);
                }
            }
           
            if (distanceToPrey <= attackDistance)
            {
                anim.SetBool("attackDistance", true);
            }
            else
            {
                anim.SetBool("attackDistance", false);
            }
        }       
    }

    void makeDamage()
    {
       prey.GetComponent<HungerAllg>().getdamage(damage);
    }

    void hungerCheck()
    {
        hungerVal = GetComponent<HungerAllg>().hunger;

        if (hungerVal < maxHunger * (whenIAmHungry / 100))
        {
            anim.SetBool("isHungry", true);
        }
        else
        {
            anim.SetBool("isHungry", false);
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

    void canSeePreyCheck()
    {
           float angle = Vector3.Angle(direction, transform.forward);
            if(direction.magnitude < visibleDistance && angle < visibleAngle * 0.5f)
            {
                 RaycastHit hit;

                     if(Physics.Raycast(transform.position, direction.normalized, out hit, visibleDistance, layer))
                     {
                         if(hit.collider.gameObject.name == prey.name)
                         {
                            GetComponent<Animator>().SetBool("canSeePrey", true);
                            canSee = true;
                         }  
                     }
            }
            else
            {
                GetComponent<Animator>().SetBool("canSeePrey", false);
                canSee = false;
            }
    }
    void canSmellPreyCheck(float distance)
    {
        if(distance < smellDistance)
        {
            GetComponent<Animator>().SetBool("canSmellPrey", true);
            canSmell = true;
        }
        else
        {
            GetComponent<Animator>().SetBool("canSmellPrey", false);
            canSmell = false;
        }
    }

    void canHearCheck( float distance)
    {
        if(distance < hearDistance && prey.GetComponent<Prey>().isMakingSound == true)
        {  
            anim.SetBool("canHearPrey", true); 
        }
        else
        {
            anim.SetBool("canHearPrey", false);
        }
    }

    void canSensePrey()
    {
        if(canSee || canSmell)
        {
            anim.SetBool("canSensePrey", true);
            canSense = true;
        }
        else
        {
            anim.SetBool("canSensePrey", false);
            canSense = false;
        }
    }
}


/*
        preys = WorldManager.preys;
        
        preys = new List<GameObject>();
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Prey")) 
        {
            preys.Add(p);
        }


  else if (preys.Count==0)
        {
            anim.SetFloat("distance", 100);
        }
        */