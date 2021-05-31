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
    float hungerVal;
    public bool hit = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(prey != null)
        {
            float distance = Vector3.Distance(transform.position, prey.transform.position);
            anim.SetFloat("distance", distance);

            direction = prey.transform.position - this.transform.position;
            Debug.DrawRay(this.transform.position, direction, Color.green);

        }
        else
        {
            Debug.Log("Gibt kein Prey mehr");
            anim.SetFloat("distance", 100);
        }


        hungerVal = GetComponent<HungerAllg>().hunger;
        anim.SetFloat("hunger", hungerVal);


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

       
    }
    
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Prey")
        {
            hit = true;
        }
        else
        {
            hit = false;
        }
 
    }
}
