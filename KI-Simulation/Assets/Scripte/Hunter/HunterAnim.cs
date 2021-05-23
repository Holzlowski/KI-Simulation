using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HunterAnim : MonoBehaviour
{

    List<GameObject> preys;
    private NavMeshAgent agent;
    public float huntRange;
    Animator anim;
    private GameObject target;

    bool isHungry;

    // Start is called before the first frame update
    void Start()
    {
        preys = new List<GameObject>();
        foreach(GameObject prey in GameObject.FindGameObjectsWithTag("Prey"))
        {
            preys.Add(prey);
        }
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject prey in preys)
        {
            float distance = Vector3.Distance(agent.transform.position, prey.transform.position);
            if (distance < huntRange)
            {
            target = prey;
            anim.SetBool("isFollowing", true);
            }
        }

        isHungry = anim.GetBool("isHungry");
        if(isHungry) 
        {
            anim.SetBool("isPatroling", true);
        }
    }

    public float getHuntRange() 
    {
        return huntRange;
    }

    public GameObject getTarget()
    {
        return target;
    }

    public void setTarget(GameObject target)
    {
        this.target = target;
    }
}