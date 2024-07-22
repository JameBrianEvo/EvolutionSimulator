using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodData
{
    public FoodScript targetFood { get; set; }
    public bool eatPlants { get; }
    public bool eatMeat { get; }

    public FoodData(bool plant, bool meat)
    {
        eatPlants = plant;
        eatMeat = meat;
        targetFood = null;
    }
}
