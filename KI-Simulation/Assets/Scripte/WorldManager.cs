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

    public GameObject plant;
    public int plantNumber;
    public float spawnTimePlant;

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
        Plant.OnDestroyPlant += spawnPlant;
    }

    private void OnDisable()
    {
        HungerAllg.OnDestroyHunter -= spawnHunter;
        HungerAllg.OnDestroyDuck -= spawnDuck;
        HungerAllg.OnDestroySheep -= spawnSheep;
        Plant.OnDestroyPlant -= spawnPlant;
    }

    // Start is called before the first frame update
    void Start()
    {
        hunters = new List<GameObject>();
        preys = new List<GameObject>();
        plants = new List<GameObject>();

        //spawnPreysAtStart();
        spawnHuntersAtStart();
        spawnPlantsAtStart();
        spawnAnimalsAtStart(sheep, sheepNest, sheepNumber, preys);
        spawnAnimalsAtStart(duck, duckNest, duckNumber, preys);
    }

    /*public static void deleteObjectFromList(GameObject animal)
    {
        string name = animal.name;
        switch (name)
        {
            case "Hunter(Clone)":
                hunters.Remove(animal);
                break;
            case "Sheep(Clone)":
                preys.Remove(animal);
                break;
            case "Duck(Clone)":
                preys.Remove(animal);
                break;
            case "Plant(Clone)":
                plants.Remove(animal);
                break;
            default:
                Debug.Log("wrong name:" + name);
                break;
        }
    }*/

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

    /*private void spawnPreysAtStart()
    {
        for (int i = 0; i < preyNumber; i++)
        {
            GameObject newPrey = Instantiate(prey, preyNest.transform.position, Quaternion.identity);
            preys.Add(newPrey);
        }
    }*/
    private void spawnPlant()
    {
        StartCoroutine(WaitPlant());
    }

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

    IEnumerator WaitPlant()
    {
        yield return new WaitForSeconds(spawnTimePlant);
        Vector3 position = new Vector3(Random.Range(-72f, 20f), 0.5f, Random.Range(-41f, 20f));
        GameObject newPlant = Instantiate(plant, position, Quaternion.identity);
        plants.Add(newPlant);
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