using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Plant : MonoBehaviour
{
    public int capacity; 
    public int capacityRangeStart = 150;
    public int capacityRangeEnd = 300;
    public static Action OnDestroyPlant;

    // Start is called before the first frame update
    void Awake()
    {
        this.capacity = UnityEngine.Random.Range(capacityRangeStart, capacityRangeEnd);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void losingCapacity(int value)
    { 
        capacity -= value;
        if(capacity <= 0)
        {
            destroyPlant();
        }
    }

    private void destroyPlant()
    {
        OnDestroyPlant?.Invoke();
        WorldManager.plants.Remove(gameObject);
        Destroy(gameObject);
    }
}
