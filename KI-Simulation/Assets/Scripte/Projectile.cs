using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float delay = 2f;

    private void Update()
    {
        Destroy(gameObject, delay);
    }
}
