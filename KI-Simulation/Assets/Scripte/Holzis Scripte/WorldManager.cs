using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static GameObject[] hidingSpots;

    // SHEEPS - PREY (wolf)
    public GameObject sheep;
    public GameObject sheepNest;
    public int sheepNumber;
    public float sheepSpawnTime;
    public static List<GameObject> sheeps {get; private set;}

    // WOLFS - HUNTER (sheep)
    public GameObject wolf;
    public GameObject wolfNest;
    public int wolfNumber;
    public float wolfSpawnTime;
    public static List<GameObject> wolfs {get; private set;}

    //DUCKS - PREY (fox)
    public GameObject duck;
    public GameObject duckNest;
    public int duckNumber;
    public float duckSpawnTime;
    public static List<GameObject> ducks {get; private set;}

    // FOXES - HUNTER (duck)
    public GameObject fox;
    public GameObject foxNest;
    public int foxNumber;
    public float foxSpawnTime;
    public static List<GameObject> foxes {get; private set;}

    // PIGS - PREY
    public GameObject pig;
    public GameObject pigNest;
    public int pigNumber;
    public float pigSpawnTime;
    public static List<GameObject> pigs {get; private set;}

    // PLANTS - for Prey
    public GameObject plant;
    public int plantNumber;
    public float spawnTimePlant;
    public static List<GameObject> plants {get; private set;}

    private void OnEnable()
    {
        HungerAllg.OnDestroyWolf += spawnWolf;
        HungerAllg.OnDestroyFox += spawnFox;

        HungerAllg.OnDestroyDuck += spawnDuck;
        HungerAllg.OnDestroySheep += spawnSheep;
        HungerAllg.OnDestroyPig += spawnPig;

        Plant.OnDestroyPlant += spawnPlant;
    }

    private void OnDisable()
    {
        HungerAllg.OnDestroyWolf -= spawnWolf;
        HungerAllg.OnDestroyFox -= spawnFox;

        HungerAllg.OnDestroyDuck -= spawnDuck;
        HungerAllg.OnDestroySheep -= spawnSheep;
        HungerAllg.OnDestroyPig += spawnPig;

        Plant.OnDestroyPlant -= spawnPlant;
    }

    // Start is called before the first frame update
    void Start()
    {
        //HIDINGSPOTS
        hidingSpots = GameObject.FindGameObjectsWithTag("hide");

        //HUNTERS
        wolfs = new List<GameObject>();
        spawnAnimalsAtStart(wolf, wolfNest, wolfNumber, wolfs);
        foxes = new List<GameObject>();
        spawnAnimalsAtStart(fox, foxNest, foxNumber, foxes);
        
        //PREYS
        sheeps = new List<GameObject>();
        spawnAnimalsAtStart(sheep, sheepNest, sheepNumber, sheeps);
        ducks = new List<GameObject>();
        spawnAnimalsAtStart(duck, duckNest, duckNumber, ducks);
        pigs = new List<GameObject>();
        spawnAnimalsAtStart(pig, pigNest, pigNumber, pigs);

        //PLANTS
        plants = new List<GameObject>();
        spawnPlantsAtStart();
    }

    private void spawnAnimalsAtStart(GameObject animal, GameObject nest, int number, List<GameObject> list)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject newAnimal = Instantiate(animal, nest.transform.position, Quaternion.identity);
            list.Add(newAnimal);
        }
    }

    private void spawnPlantsAtStart()
    {
        for (int i = 0; i < plantNumber; i++)
        {
            // spawn-area of the plants
            Vector3 position = new Vector3(Random.Range(-46f, 46f), 0.5f, Random.Range(-30f, 30f));
            GameObject newPlant = Instantiate(plant, position, Quaternion.identity);
            plants.Add(newPlant);
        }
    }

    // PLANT
    private void spawnPlant()
    {
        StartCoroutine(WaitPlant());
    }

    IEnumerator WaitPlant()
    {
        yield return new WaitForSeconds(spawnTimePlant);
        Vector3 position = new Vector3(Random.Range(-46f, 46f), 0.5f, Random.Range(-30f, 30f));
        GameObject newPlant = Instantiate(plant, position, Quaternion.identity);
        plants.Add(newPlant);
    }

    // SHEEP
    private void spawnSheep()
    {
        StartCoroutine(WaitSheep());
    }

    IEnumerator WaitSheep()
    {   
        yield return new WaitForSeconds(sheepSpawnTime);
        GameObject newSheep = Instantiate(sheep, sheepNest.transform.position, Quaternion.identity);
        sheeps.Add(newSheep); 
    }

    // DUCK
    private void spawnDuck()
    {
        StartCoroutine(WaitDuck());
    }

    IEnumerator WaitDuck()
    {   
        yield return new WaitForSeconds(duckSpawnTime);
        GameObject newDuck = Instantiate(duck, duckNest.transform.position, Quaternion.identity);
        ducks.Add(newDuck); 
    }

    // PIG
    private void spawnPig()
    {
        StartCoroutine(WaitPig());
    }

    IEnumerator WaitPig()
    {   
        yield return new WaitForSeconds(pigSpawnTime);
        GameObject newPig = Instantiate(pig, pigNest.transform.position, Quaternion.identity);
        pigs.Add(newPig); 
    }

    // WOLF
    private void spawnWolf()
    {
        StartCoroutine(WaitWolf());
    }

    IEnumerator WaitWolf()
    {
        yield return new WaitForSeconds(wolfSpawnTime);
        GameObject newWolf = Instantiate(wolf, wolfNest.transform.position, Quaternion.identity);
        wolfs.Add(newWolf);
    }

    //FOX
    private void spawnFox()
    {
        StartCoroutine(WaitFox());
    }

    IEnumerator WaitFox()
    {
        yield return new WaitForSeconds(foxSpawnTime);
        GameObject newFox = Instantiate(fox, foxNest.transform.position, Quaternion.identity);
        foxes.Add(newFox);
    }  
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