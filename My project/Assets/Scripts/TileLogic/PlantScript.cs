using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantScript : FoodScript
{
    [SerializeField]
    public float regrowRate;

    private float regrowTime = 0;

    private void Start()
    {
        regrowRate = TimeManager.Instance.daytimeSeconds * 2;

    }
    // Update is called once per frame
    void Update()
    {
        if(Time.time > regrowTime)
        {
            regrowTime = Time.time + regrowRate;
            if (numOfFood >= maxFood)
            {
                return;
            }

            numOfFood += 1;
        }
    }
}
