using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationChange : MonoBehaviour
{
   public int xPos;
   public int zPos;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character") 
        {
            xPos = Random.Range(300, 440);
            zPos = Random.Range(590, 648);
            this.gameObject.transform.position = new Vector3(xPos, 8.1f, zPos);
        }
    }
}
