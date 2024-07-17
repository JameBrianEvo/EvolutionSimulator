using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindFoodOmnivore : FindFoodStrategy
{
    public FoodScript FindFood(RangeScanner scanner, Rigidbody2D rb)
    {
        return scanner.GetNearestFood();
    }
}
