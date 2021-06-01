using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hunter : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;
    Collider col;
    public GameObject prey;

    Vector3 direction;
    float distance;
    float hungerVal;
    public float damage;
    public float healWithBite;

    //GameObject[] preys;
    List<GameObject> preys;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        /*
        for(int i = 0; i < preys.Length; i++)
        {
            preys[i] = GameObject.FindGameObjectsWithTag("Prey")[i];
        }
        */
   
        preys = new List<GameObject>();
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Prey")) 
        {
            preys.Add(p);
        }
 
    }

    // Update is called once per frame
    void Update()
    {
        hungerVal = GetComponent<HungerAllg>().hunger;
        anim.SetFloat("hunger", hungerVal);

         if(preys.Count==0)
        {
            Debug.Log("Gibt keine Preys mehr");
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
    }

    void makeDamage(float damage)
    {
       damage = this.damage;
       prey.GetComponent<HungerAllg>().getdamage(damage);
    }

}

/*
       foreach (GameObject p in preys)
            {
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
*/


/*
        //Debug.Log(hit);

        if(prey == null)
        {
            anim.SetBool("noTarget", true);
        }
        else
        {
            anim.SetBool("noTarget", false);
        }
    
       
        if(distance <= agent.stoppingDistance)
        {
            anim.SetBool("attackDistance", true);
            Debug.Log("Ich greif an");
        }
        else
        {
            anim.SetBool("attackDistance", false);
        }
       */