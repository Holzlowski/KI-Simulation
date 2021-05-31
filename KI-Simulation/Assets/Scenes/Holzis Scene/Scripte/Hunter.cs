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

    List<GameObject> preys;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        preys = new List<GameObject>();
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Prey")) 
        {
            preys.Add(p);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(preys != null)
        {
            closestPrey();
            
            direction = prey.transform.position - this.transform.position;
            Debug.DrawRay(this.transform.position, direction, Color.green);

        }
        else
        {
            Debug.Log("Gibt keine Preys mehr");
            anim.SetFloat("distance", 100);
        }

        hungerVal = GetComponent<HungerAllg>().hunger;
        anim.SetFloat("hunger", hungerVal);
    }

    void closestPrey()
    {
        float distance = Mathf.Infinity;

        foreach(GameObject p in preys)
        {
            if(p == null)
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