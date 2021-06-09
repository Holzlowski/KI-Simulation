using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class PreyAnim : MonoBehaviour
{

    public Transform target { get; set; }
    public float sleepStart;
    public float sleepEnd; 
    public float hungryValue;
    public bool hungry;

    private NavMeshAgent agent;

    public float distanceView;
    Animator anim;
    List<GameObject> plants;
    List<GameObject> hunters;
    public GameObject theHunter;
    private GameObject nest;
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
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>(); 

        plants = WorldManager.plants;
        hunters = WorldManager.hunters;
        nest = GameObject.Find("PreyNest");
    }

    // Update is called once per frame
    void Update()
    {
        plants = WorldManager.plants;
        hunters = WorldManager.hunters;
        fleeing = anim.GetBool("isFleeing");
        bool wander = anim.GetBool("isWander");
        bool hasTarget = anim.GetBool("hasTarget");
        bool eating = anim.GetBool("isEating");
        bool sleeps =anim.GetBool("isSleeping");

        float hunger = GetComponent<HungerAllg>().hunger;
        if(hunger <= hungryValue)
        {
            hungry = true;
        } else
        {
            hungry = false;
        }


        //checking if prey has to flee
        if(!fleeing) 
        {
            foreach(GameObject hunter in hunters)
            {
                if(hunter)
                {
                    float distanceToHunter = Vector3.Distance(agent.transform.position, hunter.transform.position);
                    if (distanceToHunter < distanceView)
                    {
                        target = null;
                        anim.SetBool("hasTarget", false);
                        theHunter = hunter;
                        anim.SetBool("isFleeing", true);  
                    }
                }
            }
        }
        if (!wander && !fleeing && !hasTarget && !eating && !sleeps)
        {
            anim.SetBool("isWander", true);
        }
    }

    private void TimeCheck()
    {
        if(TimeManager.Hour == sleepStart && fleeing == false)

        {
            anim.SetBool("isWander", false);
            anim.SetBool("isTired", true);
            target = nest.transform;
            anim.SetBool("hasTarget", true);
        }
    }
}
