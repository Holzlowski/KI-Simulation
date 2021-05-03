using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hunger : MonoBehaviour
{
    public Slider hungerSlider;
    public float hunger;

    public Slider lifeSlider;
    public float life;

    float maxLife = 100f;
    float maxHunger = 100f;

    // Start is called before the first frame update
    void Start()
    {
        hunger = maxHunger;
        life = maxLife;
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
         life -= 1f * Time.deltaTime;
        }
    }
}
