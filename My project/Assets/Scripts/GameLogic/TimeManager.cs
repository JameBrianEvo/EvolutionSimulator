using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance {get; private set;}
    private float Timer = 0;

    //amount of seconds
    [SerializeField] private float secondsToDay = 60f;

    //amount of seconds for daytime
    [SerializeField] private float daytimeSeconds = 45f;

    //amount of days that have pased
    public int Day { get; private set; }

    
    public bool IsDay { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        IsDay = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(Timer);
        if(Timer < secondsToDay){
            Timer += Time.deltaTime;
            if(Timer > daytimeSeconds && IsDay)
            {
                ChangeToNight();
            }
        } else {
            Timer = 0;
            Day++;
            ChangeToDay();
            WeatherManager.Instance.ChangeWeather();
        }
    }

    private void ChangeToNight()
    {
        Debug.Log("Change to night time");
        IsDay = false;
    }

    private void ChangeToDay()
    {
        Debug.Log("ChangeToDay to day time");
        IsDay = true;
    }
}
