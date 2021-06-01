using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class HungerAllg : MonoBehaviour
{
    Animator anim;

    public Slider hungerSlider;
    public float hunger;

    public Slider lifeSlider;
    public float life;
    public float minusHunger;
    float healWithBite;

    public float damage;

    private float maxLife = 100f;
    private float maxHunger = 100f;
    private bool isLow = false;
    private bool isHungry = false;

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
         life -= damage * Time.deltaTime;
        } else if (hunger <= maxHunger/2)  //if half live object is hungry (start searching)
        {
            anim.SetBool("isHungry", true);
            isHungry = true;
        } else if(hunger > maxHunger*0.6 && life < maxLife) // full Hunger heals missing life
        {
            life += 1f * Time.deltaTime;
        }

        if(isHungry && hunger > maxHunger*0.95) 
        {
            isHungry = false;
            anim.SetBool("isHungry", false);
        }

        
        if (life <= 0) {
            Destroy(gameObject);
        } else if (life <= life/4)
        {
            anim.SetBool("isLow", true);
            isLow = true;
        } else if (isLow && life > life/4)
        {
            anim.SetBool("isLow", false);
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
                anim.SetBool("isHungry", false);
            }
        }
    }

    public void getdamage(float value) {
        life -= value;
        if(life <= 0) {
            Destroy(gameObject);
        } 
        if(life > maxLife)
        {
            life = maxLife;
        }
    }
}
