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

    public float damage;

    float maxLife = 100f;
    float maxHunger = 100f;

    // Start is called before the first frame update
    void Start()
    {
        hunger = maxHunger;
        life = maxLife;
        anim = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        hungerSlider.value = hunger;
        lifeSlider.value = life;

        hunger -= 5f * Time.deltaTime; 

        if(hunger < 0f)
        {
            hunger = 0;
        }

        if(hunger == 0f)
        {
         life -= damage * Time.deltaTime;
        }

        if (hunger < maxHunger/2) {
            anim.SetBool("isPatroling", true);
        }

        if (life <= 0) {
            Destroy(gameObject);
        }
    }
}
