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

    public static List<GameObject> preys {get; private set;}
    public static List<GameObject> hunters {get; private set;}
    public static List<GameObject> plants {get; private set;}

    private bool start;

    private void OnEnable()
    {
        HungerAllg.OnDestroyHunter += spawnHunter;
        HungerAllg.OnDestroyPrey += spawnPrey;
    }

    private void OnDisable()
    {
        HungerAllg.OnDestroyHunter -= spawnHunter;
        HungerAllg.OnDestroyPrey -= spawnPrey;
    }

    // Start is called before the first frame update
    void Start()
    {
        hunters = new List<GameObject>();
        preys = new List<GameObject>();
        plants = new List<GameObject>();
        findAllPlants();
        spawnPreysAtStart();
        spawnHuntersAtStart();
    }

    private void findAllPlants()
    {
        foreach(GameObject plant in GameObject.FindGameObjectsWithTag("Plant"))
        {
            plants.Add(plant);
        }
    }

    private void spawnHuntersAtStart()
    {
        for (int i = 0; i < hunterNumber; i++)
        {
            GameObject newHunter = Instantiate(hunter, hunterNest.transform.position, Quaternion.identity);
            hunters.Add(newHunter);
        }
    }

    private void spawnPreysAtStart()
    {
        for (int i = 0; i < preyNumber; i++)
        {
            GameObject newPrey = Instantiate(prey, preyNest.transform.position, Quaternion.identity);
            preys.Add(newPrey);
        }
    }

    private void spawnHunter()
    {
        Debug.Log("hunter dead, start courotine for new one");
        StartCoroutine(WaitHunter());
    }

    private void spawnPrey()
    {
        Debug.Log("prey dead, start courotine for new one");
        StartCoroutine(WaitPrey());
    }

    IEnumerator WaitPrey()
    {   
        yield return new WaitForSeconds(spawnTimePrey);
        GameObject newPrey = Instantiate(prey, preyNest.transform.position, Quaternion.identity);
        preys.Add(newPrey); 
        Debug.Log("new Prey created and added to list");
    }

    IEnumerator WaitHunter()
    {
        yield return new WaitForSeconds(spawnTimeHunter);
        GameObject newHunter = Instantiate(hunter, hunterNest.transform.position, Quaternion.identity);
        hunters.Add(newHunter);
        Debug.Log("new Hunter created and added to list");
    }
}