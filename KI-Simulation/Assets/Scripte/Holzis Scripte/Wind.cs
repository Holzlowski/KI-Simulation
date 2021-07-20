using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public static Vector3 windDirection;
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
        
    }

    IEnumerator changedWindDirection()
    {
        while (true)
        {
            windDirection = new Vector3(Random.Range(0, 20), 0, Random.Range(0, 20));
            sound.Play();
            yield return new WaitForSeconds(Random.Range(30, 50));
        }
    }
}
