using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PreyAnim : MonoBehaviour
{

    public Transform target;
    private NavMeshAgent agent;

    public float distanceView;
    Animator anim;
    List<GameObject> plants;
    List<GameObject> hunters;
    public GameObject theHunter;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

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
        bool eating = anim.GetBool("isEating");

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
        

        if(target != null) {
            float distanceToTarget = Vector3.Distance(agent.transform.position, target.position);
            agent.SetDestination(target.position);
            if(!eating && distanceToTarget <= 2)
            {
                anim.SetBool("isEating", true);
            }
        } else {
            anim.SetBool("isWander", true);
        }
    }

    public float getDistanceView() 
    {
        return distanceView;
    }

    public List<GameObject> getPlants()
    {
        return plants;
    }

    public void setTarget(Transform target)
    {
        this.target = target;
        if(target != null) 
        {
            agent.SetDestination(target.position);
        }
    }

    public List<GameObject> getHunters()
    {
        return hunters;
    }
}
