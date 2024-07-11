using System;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public enum Weather
    {
        CLEARSKY,
        CLOUDY,
        RAINING,
        SNOWING,
        FOGGY
    }

    static Dictionary<Weather, int> weatherTemperatures = new Dictionary<Weather, int>
    {
        { Weather.CLEARSKY, 0 },
        { Weather.CLOUDY, -5 },
        { Weather.RAINING, -10 },
        { Weather.SNOWING, -15 },
        { Weather.FOGGY, 0 },
    };
    public static WeatherManager Instance { get; private set; }
    private const int defaultTemperature = 15;
    private const int dayTemperature = 10, nightTemperature = -10;
    public Weather currentWeather = Weather.CLEARSKY;

    private void Start()
    {
        Instance = this;
    }
    //everytime a new day passes, a weather is changed at random
    public void ChangeWeather()
    {
        Array values = Enum.GetValues(typeof(Weather));
        int random = UnityEngine.Random.Range(0, values.Length);
        currentWeather = (Weather)values.GetValue(random);
        Debug.Log(currentWeather);
        Debug.Log(GetTemperature());
    }

    //Temperature is calculated by weather, and time of day
    //assume temperature is based on Celcius 
    public int GetTemperature()
    {
        int temperature = defaultTemperature;
        temperature += TimeManager.Instance.IsDay ? dayTemperature : nightTemperature;
        temperature += weatherTemperatures[currentWeather];
        return temperature;
    }
}

