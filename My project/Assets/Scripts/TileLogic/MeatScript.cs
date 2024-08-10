using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatScript : FoodScript
{
    public override int EatFood()
    {
        Destroy(this);
        return energyPerFood;
    }
}
