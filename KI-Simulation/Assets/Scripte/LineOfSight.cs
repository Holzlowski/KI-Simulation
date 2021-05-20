using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public Transform target;

    float rotationSpeed = 2f;
    float speed = 2f;
    float visibleDistance = 20f;
    float visibleAngle = 90f;

    string state = "IDLE";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);

        //wenn in Sichtfeld kommt
        if (direction.magnitude < visibleDistance && angle < visibleAngle)
        {
            //the object is not tilted
            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
            this.transform.Translate(0, 0, Time.deltaTime * speed);
        }
    }
}
