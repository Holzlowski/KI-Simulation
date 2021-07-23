using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using Random = UnityEngine.Random;

public class Prey : MonoBehaviour
{
    public GameObject target{set; get;}
    public float sleepStart;
    public float sleepEnd; 
    public float hungryValue;
    public float maxHunger;
    public float wanderRadius;
    public float wanderDistance;
    public float wanderJitter;
    public List<GameObject> plants;
    public float distanceView;
    public float distanceToHunter;
    public float hidingDistance;

    [HideInInspector]
    public bool isMakingSound;
    [HideInInspector]
    public float timer;

    public bool hungry;
    public bool tired;
    public GameObject theHunter;
    public GameObject nest;

    private NavMeshAgent agent;

    [HideInInspector]
    public Vector3 chosenSpot = Vector3.zero;
    [HideInInspector]
    public Vector3 chosendirection = Vector3.zero;
    [HideInInspector]
    public GameObject chosenObject;
    [HideInInspector]
    public RaycastHit info;
    [HideInInspector]
    public AudioSource sound;

    Animator anim;
    
    List<GameObject> hunters;
    private bool fleeing;
    bool isCool;

    public static Action OnTargetChanged;

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

        StartCoroutine(animalCall());
        chosenObject = WorldManager.hidingSpots[0];


        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();

        tired = false;
        hungry = false;
        maxHunger = GetComponent<HungerAllg>().maxHunger;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.DrawLine(transform.position, transform.position + Wind.windDirection1, Color.yellow);
        getListsOfWorldManager();

        //checking if Prey is hungry
        float hunger = GetComponent<HungerAllg>().hunger;
        if(hunger <= hungryValue)
        {
            hungry = true;
        }
        if (hungry && hunger >= maxHunger*0.95)
        {
            hungry = false;
        }

        //checking if prey has to flee
        fleeing = anim.GetBool("isFleeing");
        if(!fleeing) 
        {
            foreach(GameObject hunter in hunters)
            {
                if(hunter)
                {
                    theHunter = hunter;
                    distanceToHunter = Vector3.Distance(agent.transform.position, hunter.transform.position);
                    if (distanceToHunter < distanceView)
                    { 
                        anim.SetBool("hasTarget", false);
                        target = null;
                        theHunter = hunter;
                            
                        anim.SetBool("isFleeing", true);
                    }
                }
            }

            bool sleeping = anim.GetBool("isSleeping");
            if(tired && !sleeping)
            {
                target = nest;
                anim.SetBool("hasTarget", true);
                anim.SetBool("isWander", false);
            }
        }
    }

    IEnumerator animalCall()
    {
        while (true)
        {
            isMakingSound = false;
            yield return new WaitForSeconds(Random.Range(5, 20));
            sound.Play();
            isMakingSound = true;
            yield return new WaitForSeconds(sound.clip.length);
            isMakingSound = false;
        }         
    }

    private void TimeCheck()
    {
        if(TimeManager.Hour == sleepStart)
        {
            tired = true;
        } 
        if(TimeManager.Hour == sleepEnd)
        {
            tired = false;
        }
    }

    private void getListsOfWorldManager()
    {
        string name = gameObject.name;
        switch (name)
        {
            case "Sheep(Clone)":
                plants = WorldManager.plants;
                hunters = WorldManager.wolfs;
                nest = GameObject.Find("SheepNest");
                break;
            case "Duck(Clone)":
                plants = WorldManager.plants;
                hunters = WorldManager.foxes;
                nest = GameObject.Find("DuckNest");
                break;
            default:
                Debug.Log("wrong name:" + name);
                break;
        }
    }

    public bool checkIfHunterCanSmellMe(Vector3 hunterPosition)
    {
        Vector3 direction = hunterPosition - transform.position;
        float angle = Vector3.Angle(Wind.windDirection1, direction);

        if(direction.magnitude < Wind.windDirection1.magnitude && angle < 40 * 0.5f)
        {
            //Debug.Log("Kann gerochen werden");
            return true;
        }
        else
        {
            //Debug.Log("Ich rieche nichts");
            return false;
        }
    }

    public void closestHidingSpot()
    {
        float distance = Mathf.Infinity;

        for(int i = 0; i < WorldManager.hidingSpots.Length; i++)
        {
            Vector3 hideDirection = WorldManager.hidingSpots[i].transform.position - theHunter.transform.position;
            Vector3 hidePosition = WorldManager.hidingSpots[i].transform.position + hideDirection.normalized * 5;

            if(Vector3.Distance(this.transform.position, hidePosition) < distance)
            {
                chosenSpot = hidePosition;
                chosendirection = hideDirection;
                chosenObject = WorldManager.hidingSpots[i];
                distance = Vector3.Distance(this.transform.position, hidePosition);
            }
        }
    }

    public void hide()
    {
        //Kollider vom Baum, hinter der sich die Prey verstecken will
        Collider hideCol = chosenObject.GetComponent<Collider>();
        Ray backRay = new Ray(chosenSpot, -chosenSpot.normalized);
        RaycastHit info;
        float aDistance = 100f;
        //Ein Ray vom Baumcollider der vom Hunter ausgesehen hinter dem Baum langführt
        hideCol.Raycast(backRay, out info, aDistance);

        //Ort zum Verstecken befindet sich mit Abstand(*5) vom Collider 
        agent.SetDestination(info.point + chosendirection * 5);
    }

    void hideCheck()
    {
        if (distanceToHunter < hidingDistance)
        {
            closestHidingSpot();
            anim.SetBool("haveToHide", true);
        }
        else
        {
            anim.SetBool("haveToHide", true);
        }
    }
}
