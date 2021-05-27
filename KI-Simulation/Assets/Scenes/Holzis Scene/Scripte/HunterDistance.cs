using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HunterDistance : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;
    public GameObject prey;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, prey.transform.position);
        anim.SetFloat("distance", distance);

       /*
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

        direction = prey.transform.position - this.transform.position;
        Debug.DrawRay(this.transform.position, direction, Color.green);
    }
}
