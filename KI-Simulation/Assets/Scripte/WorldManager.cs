using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public GameObject sheep;
    public GameObject sheepNest;
    public int sheepNumber;
    public float sheepSpawnTime;

    public GameObject duck;
    public GameObject duckNest;
    public int duckNumber;
    public float duckSpawnTime;

    public GameObject hunter;
    public GameObject hunterNest;
    public int hunterNumber;
    public float spawnTimeHunter;

    public static List<GameObject> preys {get; private set;}
    public static List<GameObject> hunters {get; private set;}
    public static List<GameObject> plants {get; private set;}

    private bool start;

    public List<string> tags;

    private void OnEnable()
    {
        HungerAllg.OnDestroyHunter += spawnHunter;
        HungerAllg.OnDestroyDuck += spawnDuck;
        HungerAllg.OnDestroySheep += spawnSheep;
    }

    private void OnDisable()
    {
        HungerAllg.OnDestroyHunter -= spawnHunter;
        HungerAllg.OnDestroyDuck -= spawnDuck;
        HungerAllg.OnDestroySheep -= spawnSheep;
    }

    // Start is called before the first frame update
    void Start()
    {
        hunters = new List<GameObject>();
        preys = new List<GameObject>();
        plants = new List<GameObject>();
        findAllPlants();

        //spawnPreysAtStart();
        spawnHuntersAtStart();
        spawnAnimalsAtStart(sheep, sheepNest, sheepNumber, preys);
        spawnAnimalsAtStart(duck, duckNest, duckNumber, preys);
    }
    /*
    public List<GameObject> createListWithTag(string tagName)
    {
        GameObject[] arrayWithTag = GameObject.FindGameObjectsWithTag("" + tagName);
        List<GameObject> listWithtag = new List<GameObject>();

        if (tag.Length == 0)
        {
            Debug.Log("Es gibt keine Objekte mit diesem Tag.");
        }
        else
        {
            
            listWithtag.AddRange(arrayWithTag);
        }
        return listWithtag;
    }
   

     string listNameCreator(string listName)
    {
        return listName;
    }
     */

    private void findAllPlants()
    {
        foreach(GameObject plant in GameObject.FindGameObjectsWithTag("Plant"))
        {
            plants.Add(plant);
        }
    }

    private void spawnAnimalsAtStart(GameObject animal, GameObject nest, int number, List<GameObject> list)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject newAnimal = Instantiate(animal, nest.transform.position, Quaternion.identity);
            list.Add(newAnimal);
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

    /*private void spawnPreysAtStart()
    {
        for (int i = 0; i < preyNumber; i++)
        {
            GameObject newPrey = Instantiate(prey, preyNest.transform.position, Quaternion.identity);
            preys.Add(newPrey);
        }
    }*/

    private void spawnHunter()
    {
        StartCoroutine(WaitHunter());
    }

    private void spawnSheep()
    {
        StartCoroutine(WaitSheep());
    }
    private void spawnDuck()
    {
        StartCoroutine(WaitDuck());
    }

    IEnumerator WaitSheep()
    {   
        yield return new WaitForSeconds(sheepSpawnTime);
        GameObject newSheep = Instantiate(sheep, sheepNest.transform.position, Quaternion.identity);
        preys.Add(newSheep); 
    }

    IEnumerator WaitDuck()
    {   
        yield return new WaitForSeconds(duckSpawnTime);
        GameObject newDuck = Instantiate(duck, duckNest.transform.position, Quaternion.identity);
        preys.Add(newDuck); 
    }

    IEnumerator WaitHunter()
    {
        yield return new WaitForSeconds(spawnTimeHunter);
        GameObject newHunter = Instantiate(hunter, hunterNest.transform.position, Quaternion.identity);
        hunters.Add(newHunter);
    }
}