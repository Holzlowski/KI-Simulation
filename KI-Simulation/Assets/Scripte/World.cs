using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class World 
{
    private static readonly World instance = new World();

    private static GameObject[] hidingSpots;
    private static GameObject[] preys;
    private static GameObject[] hunters;
    private static GameObject[] plants;

    static World()
    {
        //hidingSpots = GameObject.FindGameObjectsWithTag("hide");

        hunters = GameObject.FindGameObjectsWithTag("Hunter");

        preys = GameObject.FindGameObjectsWithTag("Prey");

        plants = GameObject.FindGameObjectsWithTag("Plant");
    }

    private World()
    {

    }

    public static World Instance
    {
        get { return instance; }
    }

    public GameObject[] GetHidingSpots()
    {
        return hidingSpots;
    }

    public GameObject[] GetPreys()
    {
        return preys;
    }

    public GameObject[] GetHunters()
    {
        return hunters;
    }

    public GameObject[] GetPlants()
    {
        return plants;
    }
}
