using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class PreyAnim : MonoBehaviour
{

    public Transform target { get; set; }
    private NavMeshAgent agent;

    public float distanceView { get; private set; }
    Animator anim;
    List<GameObject> plants;
    List<GameObject> hunters;
    public GameObject theHunter;

    public static Action OnTargetChanged;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        distanceView = 8f;

        hunters = new List<GameObject>();
        foreach(GameObject hunter in GameObject.FindGameObjectsWithTag("Hunter"))
        {
            hunters.Add(hunter);
        }

        plants = new List<GameObject>();
        foreach(GameObject plant in GameObject.FindGameObjectsWithTag("Plant"))
        {
            plants.Add(plant);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool fleeing = anim.GetBool("isFleeing");
        bool wander = anim.GetBool("isWander");
        bool hasTarget = anim.GetBool("hasTarget");
        bool eating = anim.GetBool("isEating");

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
                        theHunter = hunter;
                        anim.SetBool("isFleeing", true);  
                    }
                }
            }
        }
        if (!wander && !fleeing && !hasTarget && !eating)
        {
            anim.SetBool("isWander", true);
        }
    }

    public List<GameObject> getPlants()
    {
        return plants;
    }

    public List<GameObject> getHunters()
    {
        return hunters;
    }
}
