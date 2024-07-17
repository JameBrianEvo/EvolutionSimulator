using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindFoodHerbivore : FindFoodStrategy
{
    public FoodScript FindFood(RangeScanner scanner, Rigidbody2D rb)
    {
        HashSet<FoodScript> foods = scanner.GetFoods();
        float distance = float.MaxValue;
        FoodScript nearest_food = null;
        foreach (FoodScript food in foods)
        {
            //Debug.Log(distance);
            //Debug.Log(Vector2.Distance(food.GetPosition(), transform.position));
            if (Vector2.Distance(food.GetPosition(), rb.position) < distance && food.IsPlant())
            {
                nearest_food = food;
            }
        }

        return nearest_food;
    }
}
