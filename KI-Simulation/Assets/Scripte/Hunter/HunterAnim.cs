using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HunterAnim : MonoBehaviour
{

    private Transform prey;
    private NavMeshAgent agent;
    public float huntRange;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        prey = GameObject.FindGameObjectWithTag("Prey").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(agent.transform.position, prey.position);
        if (distance < huntRange)
        {
            anim.SetBool("isFollowing", true);
        }
    }

    public float getHuntRange() 
    {
        return huntRange;
    }
}