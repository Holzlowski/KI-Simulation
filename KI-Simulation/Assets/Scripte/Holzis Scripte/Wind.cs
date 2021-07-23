using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public static Vector3 windDirection1;
    Vector3 windDirection2;
    public Transform windShield;
    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        StartCoroutine("changedWindDirection");
    }

    // Update is called once per frame
    void Update()
    {
        windDirection1 = Vector3.Slerp(windDirection1, windDirection2, Time.deltaTime * 0.5f);
        windShield.rotation = Quaternion.Slerp(windShield.transform.rotation, Quaternion.LookRotation(windDirection2), Time.deltaTime * 0.5f);
    }

    IEnumerator changedWindDirection()
    {
        while (true)
        {   
            windDirection2 = new Vector3(Random.Range(-21, 21), 0, Random.Range(-21, 21));
            sound.Play();
            yield return new WaitForSeconds(Random.Range(30,50));
        }
    }
}
