using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;
    
    public static int Minute { get; private set; }
    public static int Hour { get; private set; }

    private float minuteToRealTime = 0.5f;
    private float timer;

    void Start()
    {
        Minute = 50;
        Hour = 23;
        timer = minuteToRealTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Minute++;
            OnMinuteChanged?.Invoke();

            if (Minute >= 60)
            {        
                Hour++;
                if (Hour >= 24)
                {
                    Hour = 0;
                }
                Minute = 0;
                OnHourChanged?.Invoke();
            }
            
            timer = minuteToRealTime;
        }
    }
}
