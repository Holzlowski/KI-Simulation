using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public GameObject prey;
    public GameObject preyNest;
    public int preyNumber;
    public float spawnTimePrey;

    public GameObject hunter;
    public GameObject hunterNest;
    public int hunterNumber;
    public float spawnTimeHunter;

    public static GameObject[] preys {get; private set;}
    public static GameObject[] hunters {get; private set;}
    public static GameObject[] plants {get; private set;}

    private bool start;

    private void OnEnable()
    {
        HungerAllg.OnDestroyHunter += spawnHunters;
        HungerAllg.OnDestroyPrey += spawnPreys;
    }

    private void OnDisable()
    {
        HungerAllg.OnDestroyHunter -= spawnHunters;
        HungerAllg.OnDestroyPrey -= spawnPreys;
    }

    // Start is called before the first frame update
    void Start()
    {
        hunters = new GameObject[hunterNumber];
        preys = new GameObject[preyNumber];
        plants = GameObject.FindGameObjectsWithTag("Plant");

        start = true;
        spawnPreys();
        spawnHunters();
    }

    private void spawnHunters()
    {
        for (int i = 0; i < hunterNumber; i++)
        {
            if(hunters[i] == null)
            {
                GameObject placeholder = new GameObject();
                placeholder.name = "newbornHunter";
                hunters[i] = placeholder;
                StartCoroutine(WaitHunter(i));
            }
        }
    }

    private void spawnPreys()
    {
        for (int i = 0; i < preyNumber; i++)
        {
            if(preys[i] == null)
            {
                GameObject placeholder = new GameObject();
                placeholder.name = "newbornPrey";
                preys[i] = placeholder;
                StartCoroutine(WaitPrey(i));
            }
        }
    }

    IEnumerator WaitPrey(int i)
    {   
        yield return new WaitForSeconds(spawnTimePrey);
        GameObject newPrey = Instantiate(prey, preyNest.transform.position, Quaternion.identity);
        Destroy(preys[i]);
        preys[i] = newPrey; 
    }

    IEnumerator WaitHunter(int i)
    {
        yield return new WaitForSeconds(spawnTimeHunter);
        GameObject newHunter = Instantiate(hunter, hunterNest.transform.position, Quaternion.identity);
        Destroy(hunters[i]);
        hunters[i] = newHunter;
    }
}