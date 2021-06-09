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
    public float maxHunger;
    public float damage;
    public float healWithBite;
    [Range(10f,90f)]
    public float whenIAmHungry = 50f;
    List<GameObject> preys;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        maxHunger = GetComponent<HungerAllg>().maxHunger;

        //preys = WorldManager.preys;
   
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
        //anim.SetFloat("hunger", hungerVal);
        hungerCheck();

         if (preys.Count==0)
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

    void hungerCheck()
    {
        if (hungerVal < maxHunger * (whenIAmHungry / 100))
        {
            anim.SetBool("isHungry", true);
            Debug.Log("Hungrig");
        }
        else
        {
            anim.SetBool("isHungry", false);
            Debug.Log("Nicht Hungrig");
        }
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