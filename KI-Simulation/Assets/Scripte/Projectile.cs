using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float delay = 2f;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    /*
    private void Update()
    {
        Destroy(gameObject, delay);
    }
    */
}
