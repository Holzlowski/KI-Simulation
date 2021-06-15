using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Prey : MonoBehaviour
{
    public GameObject target{set; get;}
    public float sleepStart;
    public float sleepEnd; 
    public float hungryValue;
    public float maxHunger;
    public float wanderRadius;
    public float wanderDistance;
    public float wanderJitter;
    public List<GameObject> plants;
    public float distanceView;

    public bool hungry;
    public bool tired;
    public GameObject theHunter;
    public GameObject nest;

    private NavMeshAgent agent;

    
    Animator anim;
    List<GameObject> hunters;
    private bool fleeing;

    public static Action OnTargetChanged;

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

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>(); 

        tired = false;
        hungry = false;
        maxHunger = GetComponent<HungerAllg>().maxHunger;
    }

    // Update is called once per frame
    void Update()
    {
        getListsOfWorldManager();
        
        //checking if Prey is hungry
        float hunger = GetComponent<HungerAllg>().hunger;
        if(hunger <= hungryValue)
        {
            hungry = true;
        }
        if (hungry && hunger >= maxHunger*0.95)
        {
            hungry = false;
        }


        //checking if prey has to flee
        fleeing = anim.GetBool("isFleeing");
        if(!fleeing) 
        {
            foreach(GameObject hunter in hunters)
            {
                if(hunter)
                {
                    theHunter = hunter;
                    float distanceToHunter = Vector3.Distance(agent.transform.position, hunter.transform.position);
                    if (distanceToHunter < distanceView)
                    {
                        anim.SetBool("hasTarget", false);
                        target = null;
                        theHunter = hunter;
                        anim.SetBool("isFleeing", true);
                    }
                }
            }
            bool sleeping = anim.GetBool("isSleeping");
            if(tired && !sleeping)
            {
                target = nest;
                anim.SetBool("hasTarget", true);
                anim.SetBool("isWander", false);
            }
        }
    }

    private void TimeCheck()
    {
        if(TimeManager.Hour == sleepStart)
        {
            tired = true;
        } 
        if(TimeManager.Hour == sleepEnd)
        {
            tired = false;
        }
    }

    private void getListsOfWorldManager()
    {
        string name = gameObject.name;
        switch (name)
        {
            case "Sheep(Clone)":
                plants = WorldManager.plants;
                hunters = WorldManager.wolfs;
                nest = GameObject.Find("SheepNest");
                break;
            case "Duck(Clone)":
                plants = WorldManager.plants;
                hunters = WorldManager.foxes;
                nest = GameObject.Find("DuckNest");
                break;
            default:
                Debug.Log("wrong name:" + name);
                break;
        }
    }
}
