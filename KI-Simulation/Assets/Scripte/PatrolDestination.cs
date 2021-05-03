using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolDestination : MonoBehaviour
{
   public int xPos;
   public int zPos;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character") 
        {
            xPos = Random.Range(-13, 13);
            zPos = Random.Range(-13, 13);
            this.gameObject.transform.position = new Vector3(xPos, 0.6f, zPos);
        }
    }
}
