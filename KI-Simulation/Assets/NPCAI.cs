using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAI : MonoBehaviour
{
    Animator anim;
    public GameObject target;
    public GameObject bullet;
    public GameObject mouth;

    public GameObject GetTarget()
    {
        return target;
    }

    void Fire()
    {
        GameObject b = Instantiate(bullet, mouth.transform.position, mouth.transform.rotation);
        b.GetComponent<Rigidbody>().AddForce(mouth.transform.forward * 500);
    }

    public void StopFiring()
    {
        CancelInvoke("Fire");
    }

    public void StartFiring()
    {
        InvokeRepeating("Fire", 0.5f, 0.5f);
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetFloat("distance", Vector3.Distance(transform.position, target.transform.position));
    }

}

