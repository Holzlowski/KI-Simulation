using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hunter : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;
    Wind wind;

    [HideInInspector]
    public GameObject prey;
    [HideInInspector]
    public GameObject nest;
    List<GameObject> preys;
    [HideInInspector]
    public LayerMask layer;
    
    [HideInInspector]
    public Vector3 direction;
    //[HideInInspector]
    //public float distanceToPrey;

    [HideInInspector]
    public float orginalSpeed;
    [HideInInspector]
    public Vector3 randomPoint;
    [HideInInspector]
    public Vector3 searchingPosition;

    
  

    bool canSee, canSmell, canHear, canSense;

    [Header("Hunger Settings")]
    public float maxHunger;
    [Range(10f, 90f)]
    public float whenIAmHungry = 50f;
    float hungerVal;

    [Header("Movement Settings")]
    public float speed;
    public float huntingSpeed;
    public float rotSpeed;
    public float acceleration;
    public float wanderDistance, wanderRadius, wanderJitter;
    public float searchingRadius;

    [Header("Perception Settings")]
    public float visibleRange = 20f;
    public float visibleAngle = 60f;
    public float smellRange = 20f;
    public float hearRange = 40f;
    [HideInInspector]
    public Vector3 noisePosition;

    [Header("Sleep Settings")]
    public float sleepStart;
    public float sleepEnd;

    [Header("Attack and Healing Settings")]
    public float attackDistance;
    public float damage;
    public float healWithBite;

    private void OnEnable()
    {
        //TimeManager.OnMinuteChanged += TimeCheck;
        TimeManager.OnHourChanged += TimeCheck;
    }

    private void OnDisable()
    {
        //TimeManager.OnMinuteChanged -= TimeCheck;
        TimeManager.OnHourChanged -= TimeCheck;
    }
    // Start is called before the first frame update
    void Start()
    {
        getListsOfWorldManager();
        wind = FindObjectOfType<Wind>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        maxHunger = GetComponent<HungerAllg>().maxHunger;
        agent.stoppingDistance = attackDistance;
        agent.speed = this.speed;
        orginalSpeed = agent.speed;
    }

    
    // Update is called once per frame
    void Update()
    {
        getListsOfWorldManager();
        hungerCheck();
        closestPrey();

        if(prey != null)
        {
            float distance = Vector3.Distance(prey.transform.position, transform.position);
            direction = prey.transform.position - this.transform.position;
            canSeePreyCheck();
            canSmellPreyCheck(distance);
            canHearCheck(distance);
            canSensePrey();

            if (canSee == true)
            {
                Debug.DrawRay(this.transform.position, direction, Color.green);
            }
        }  
    }

    void closestPrey()
    {
        if(preys.Count > 0)
        {
            float distanceToPrey = Mathf.Infinity;

            for (int i = 0; i < preys.Count; i++)
            {
                GameObject p = preys[i];

                if (p == null)
                {
                    preys.Remove(p);
                }
                else if (Vector3.Distance(transform.position, p.transform.position) < distanceToPrey)
                {
                    prey = p;
                    distanceToPrey = Vector3.Distance(transform.position, p.transform.position);
                    anim.SetFloat("distance", distanceToPrey);

                    if (distanceToPrey <= attackDistance)
                    {
                        anim.SetBool("attackDistance", true);
                    }
                    else
                    {
                        anim.SetBool("attackDistance", false);
                    }
                }
            }
           
           
        }       
    }

    void makeDamage()
    {
        if(prey != null)
        {
            prey.GetComponent<HungerAllg>().getdamage(damage);
        }   
    }

    void searchRandomPoint(Vector3 searchingPosition)
    {
        Vector3 randomPoint = Random.insideUnitSphere * searchingRadius + searchingPosition;
        randomPoint.y = 0;
        this.randomPoint = randomPoint;
    }

    IEnumerator lookingAround()
    {
        while (true)
        {
            searchRandomPoint(noisePosition);
            yield return new WaitForSeconds(Random.Range(0.3f, 2f));
        }
    }
    IEnumerator stopLookingAround()
    {
        yield return new WaitForSeconds(Random.Range(3f, 10f));
        anim.SetBool("stopLookingAround", true);
        Debug.Log("Fertsch mit Gucke");
    }

    void hungerCheck()
    {
        hungerVal = GetComponent<HungerAllg>().hunger;

        if (hungerVal < maxHunger * (whenIAmHungry / 100))
        {
            anim.SetBool("isHungry", true);
        }
        else
        {
            anim.SetBool("isHungry", false);
        }
    }
    private void TimeCheck()
    {
        if (TimeManager.Hour == sleepStart)
        {
            anim.SetBool("isTired", true);
        }
        if(TimeManager.Hour == sleepEnd)
        {
            anim.SetBool("isTired", false);
        }
    }

    private void getListsOfWorldManager()
    {
        string name = gameObject.name;
        switch (name)
        {
            case "Wolf(Clone)":
                preys = WorldManager.sheeps;
                nest = GameObject.Find("WolfNest");
                break;
            case "Wolf Smell(Clone)":
                preys = WorldManager.sheeps;
                nest = GameObject.Find("WolfNest");
                break;
            case "Fox(Clone)":
                preys = WorldManager.ducks;
                nest = GameObject.Find("FoxNest");
                break;
            default:
                Debug.Log("wrong name:" + name);
                break;
        }
    }

    void canSeePreyCheck()
    {
           float angle = Vector3.Angle(direction, transform.forward);
            if(direction.magnitude < visibleRange && angle < visibleAngle * 0.5f)
            {
                 RaycastHit hit;

                     if(Physics.Raycast(transform.position, direction.normalized, out hit, visibleRange, layer))
                     {
                         if(hit.collider.gameObject.name == prey.name)
                         {
                            GetComponent<Animator>().SetBool("canSeePrey", true);
                            canSee = true;
                         }  
                     }
            }
            else
            {
                GetComponent<Animator>().SetBool("canSeePrey", false);
                canSee = false;
            }
    }
    void canSmellPreyCheck(float distance)
    {
        
        //float smellDistance = Vector3.Distance(prey.transform.position + Wind.windDirection, transform.position);
        //Debug.DrawLine(transform.position, prey.transform.position + Wind.windDirection - transform.position, Color.yellow);
        
        if(distance < smellRange || prey.GetComponent<Prey>().checkIfHunterCanSmellMe(transform.position) == true)
        {
            GetComponent<Animator>().SetBool("canSmellPrey", true);
            canSmell = true;
        }
        else
        {
            GetComponent<Animator>().SetBool("canSmellPrey", false);
            canSmell = false;
        }
    }

    void canHearCheck( float distance)
    {
        if(distance < hearRange && prey.GetComponent<Prey>().isMakingSound == true)
        {  
            anim.SetBool("canHearPrey", true); 
        }
        else
        {
            anim.SetBool("canHearPrey", false);
        }
    }

    void canSensePrey()
    {
        if(canSee || canSmell)
        {
            anim.SetBool("canSensePrey", true);
            canSense = true;
        }
        else
        {
            anim.SetBool("canSensePrey", false);
            canSense = false;
        }
    }
}
