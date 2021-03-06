using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;

public class HungerAllg : MonoBehaviour
{
    Animator anim;
    public static Action OnDestroyWolf;
    public static Action OnDestroyFox;
    public static Action OnDestroySheep;
    public static Action OnDestroyDuck;
    public static Action OnDestroyPig;

    public Slider hungerSlider;
    public float hunger;

    public Slider lifeSlider;
    public float life;
    public float minusHunger;
    float healWithBite;

    public float starvingDamage;

    private float maxLife = 100f;
    [HideInInspector]
    public float maxHunger = 100f;
    [Range(10f, 90f)]
    public float whenIGetHealed = 60f;

    // Start is called before the first frame update
    void Start()
    {
        hunger = maxHunger;
        life = maxLife;
        anim = GetComponent<Animator>();
       
        if(GetComponent<Hunter>() != null)
        {
            this.healWithBite = GetComponent<Hunter>().healWithBite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        hungerSlider.value = hunger;
        lifeSlider.value = life;

        hunger -= minusHunger * Time.deltaTime; 

        if(hunger < 0f)
        {
            hunger = 0;
        }
        if(hunger > maxHunger)
        {
            hunger = maxHunger;
        }
        if(hunger == 0f) //hunger empty does deamage
        {
         life -= starvingDamage * Time.deltaTime;

        }
        if (life > maxLife)
        {
            life = maxLife;
        }
        else if(hunger > maxHunger*(whenIGetHealed/100) && life < maxLife) // full Hunger heals missing life
        {
            life += 1f * Time.deltaTime;
        }
        
        if (life <= 0) {
            destroyObject();
        } 
    }

    public void eating(float value) {
        if(healWithBite != 0)
        {
            value = healWithBite;
        }
        if(hunger < maxHunger) {
            this.hunger += value;
            if (hunger > maxHunger) {
                hunger = maxHunger;
            }
        }
    }

    public void getdamage(float value) {
        life -= value;
        if(life <= 0) {
            destroyObject();
        } 
        if(life > maxLife)
        {
            life = maxLife;
        }
    }

    private void destroyObject()
    {
        string name = gameObject.name;
        switch (name)
        {
            case "Wolf(Clone)":
                OnDestroyWolf?.Invoke();
                WorldManager.wolfs.Remove(gameObject);
                break;
            case "Fox(Clone)":
                OnDestroyFox?.Invoke();
                WorldManager.foxes.Remove(gameObject);
                break;
            case "Sheep(Clone)":
                OnDestroySheep?.Invoke();
                WorldManager.sheeps.Remove(gameObject);
                break;
            case "Duck(Clone)":
                OnDestroyDuck?.Invoke();
                WorldManager.ducks.Remove(gameObject);
                break;
            case "Pig(Clone)":
                OnDestroyPig?.Invoke();
                WorldManager.pigs.Remove(gameObject);
                break;
            default:
                Debug.Log("wrong name:" + name);
                break;
        }
        Destroy(gameObject);
    }
}
