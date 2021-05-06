using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PreyAnim : MonoBehaviour
{

    private Transform hunter;
    private NavMeshAgent agent;
    public float DistanceFlee;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        hunter = GameObject.FindGameObjectWithTag("Hunter").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(agent.transform.position, hunter.position);
        if (distance < DistanceFlee)
        {
            anim.SetBool("isFleeing", true);
            anim.SetBool("isPatroling", false);

        } else {
            anim.SetBool("isPatroling", true);
            anim.SetBool("isFleeing", false);
        }
    }
}
