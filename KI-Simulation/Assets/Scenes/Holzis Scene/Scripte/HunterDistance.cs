using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterDistance : MonoBehaviour
{
    Animator anim;
    public GameObject prey;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = prey.transform.position - this.transform.position;
        Debug.DrawRay(this.transform.position, direction, Color.green);
        anim.SetFloat("distance", Vector3.Distance(transform.position, prey.transform.position));
    }
}
