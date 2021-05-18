using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target;

    bool cooldown = false;

    Vector3 wanderTarget = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    void Flee(Vector3 location)
    {
        Vector3 fleeVector = location - this.transform.position;
        agent.SetDestination(this.transform.position - fleeVector);
    }

    void Pursue()
    {
        Vector3 targetDir = target.transform.position - this.transform.position;

        //Angle between forward directions from the agent and the target
        float realativeHeading = Vector3.Angle(this.transform.forward, this.transform.TransformVector(target.transform.forward));
        //Angle between Agent and the Target
        float toTarget = Vector3.Angle(this.transform.forward, this.transform.TransformVector(targetDir));

        if ((toTarget > 90 && realativeHeading < 20 ) || target.GetComponent<Drive>().currentSpeed < 0.01f)
        {
            Seek(target.transform.position);
            return;
        }

        float lookAhead = targetDir.magnitude / (agent.speed + target.GetComponent<Drive>().currentSpeed);
        Seek(target.transform.position + target.transform.forward * lookAhead);
    }

    void Evade()
    {
        Vector3 targetDir = target.transform.position - this.transform.position;
        float lookAhead = targetDir.magnitude / (agent.speed + target.GetComponent<Drive>().currentSpeed);
        Flee(target.transform.position + target.transform.forward * lookAhead);
    }

    void Wander()
    {
        float wanderRadius = 10;
        float wanderDistance = 10;
        float wanderJitter = 10;

        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0, Random.Range(-1.0f, 1.0f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);

        Seek(targetWorld);
    }

    void Hide()
    {
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;

        for (int i = 0; i < World.Instance.GetHidingSpots().Length; i++)
        {
            //calculating spot to hide
            Vector3 hideDirection = World.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePosition = World.Instance.GetHidingSpots()[i].transform.position + hideDirection.normalized * 5;

            //checks if spot is the closest
            if(Vector3.Distance(this.transform.position, hidePosition) < distance)
            {
                chosenSpot = hidePosition;
                distance = Vector3.Distance(this.transform.position, hidePosition);
            }
        }

        //agent.SetDestination(chosenSpot);
        Seek(chosenSpot);

    }

    void CleverHide()
    {
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosenDirection = Vector3.zero;
        GameObject chosenGO = World.Instance.GetHidingSpots()[0];

        for (int i = 0; i < World.Instance.GetHidingSpots().Length; i++)
        {
            //calculating spot to hide
            Vector3 hideDirection = World.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePosition = World.Instance.GetHidingSpots()[i].transform.position + hideDirection.normalized * 5;

            //checks if spot is the closest
            if (Vector3.Distance(this.transform.position, hidePosition) < distance)
            {
                chosenSpot = hidePosition;
                chosenDirection = hideDirection;
                chosenGO = World.Instance.GetHidingSpots()[i];
                distance = Vector3.Distance(this.transform.position, hidePosition);
            }
        }

        //calculating spot behind an object depends on size of collider
        Collider hideCollider = chosenGO.GetComponent<Collider>();
        Ray backRay = new Ray(chosenSpot, -chosenDirection.normalized);
        RaycastHit info;
        float rayDistance = 100.0f;
        hideCollider.Raycast(backRay, out info, rayDistance);

        Seek(info.point + chosenDirection.normalized * 5);

    }

    bool CanSeeTarget()
    {
        RaycastHit raycastInfo;
        Vector3 rayToTarget = target.transform.position - this.transform.position;
        if(Physics.Raycast(this.transform.position, rayToTarget, out raycastInfo))
        {
            if (raycastInfo.transform.gameObject.tag == "cop")
            {
                return true;
            }
        }
        return false;
    }

    bool TargetCanSeeMe()
    {
        Vector3 toAgent = this.transform.position - target.transform.position;
        float lookingAngle = Vector3.Angle(target.transform.forward, toAgent);

        if (lookingAngle < 60)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void BehaviourCoolDown()
    {
        cooldown = false;
    }

    bool TargetInRange()
    {
        if(Vector3.Distance(this.transform.position, target.transform.position) < 10)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!cooldown)
        {
            if (!TargetInRange())
            {
                Wander();
            }
            else if (CanSeeTarget() && TargetCanSeeMe())
            {
                CleverHide();
                cooldown = true;
                Invoke("BehaviourCoolDown", 5);
            }
            else
            {
                Pursue();
            }
        }
    }
}
