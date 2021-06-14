using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreateAnimal : MonoBehaviour
{
    public string animalName;
    public enum myEnum
    {
        hunter,
        prey
    }
    public myEnum typ;
    public Color myColor;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = animalName;
        gameObject.AddComponent<NavMeshAgent>();
        gameObject.AddComponent<HungerAllg>();
        //gameObject.AddComponent<Animator>();
        Animator anim = GetComponent<Animator>();
        if (typ == myEnum.hunter)
        {
            gameObject.AddComponent<Hunter>();
            var form = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            form.GetComponent<Renderer>().material.color = myColor;
            form.transform.parent = gameObject.transform;
            form.transform.position = new Vector3(0, 0, 0);
            form.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
            this.anim.runtimeAnimatorController = Instantiate(Resources.Load("Assets/Scenes/Holzis Scene/Hunter.controller")) as RuntimeAnimatorController;

        }
        if (typ == myEnum.prey)
        {
            gameObject.AddComponent<Prey>();
            var form = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            form.GetComponent<Renderer>().material.color = myColor;
            form.transform.parent = gameObject.transform;
            form.transform.position = new Vector3(0, 0, 0);
            form.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
            anim.runtimeAnimatorController = Resources.Load("PreyAnimator") as RuntimeAnimatorController;
        }
    }

    // Update is called once per frame
    void Update()
    {
         
    }
}
