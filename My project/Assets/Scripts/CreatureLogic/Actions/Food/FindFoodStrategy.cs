using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface FindFoodStrategy
{
    public FoodScript FindFood(RangeScanner scanner, Rigidbody2D rb);
}
