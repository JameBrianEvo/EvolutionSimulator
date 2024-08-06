using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
//Use of this class should be only for unique traits
//Traits such as cannibalism are put in FoodData and not This
public class TraitBuilder
{
    CreatureData data;
    Dictionary<Traits, Dictionary<int, int>> traits;

    public TraitBuilder(CreatureData data)
    {
        this.data = data;
        traits = new();
    }


    public TraitBuilder BuildMovementTraits()
    {
        Dictionary<int, int> movementTraits = new();
        //can swim made by some random 50%
        movementTraits.Add((int)MovementTraits.WATER, Random.Range(0,Enum.GetNames(typeof(WaterMovement)).Length));
        traits.Add(Traits.MOVEMENT, movementTraits);
        return this;
    }

    public TraitBuilder BuildSleepingTraits()
    {
        //sleeping pattern
        //random choice between night or day
        Dictionary<int, int> sleepingTraits = new();
        sleepingTraits.Add((int) SleepTraits.SLEEPPATTERN, Random.Range(0,Enum.GetNames(typeof(SleepPattern)).Length));
        traits.Add(Traits.SLEEPING, sleepingTraits);
        return this;
    }

    public TraitBuilder BuildFoodTraits()
    {
        //diet
        //random choice between veggies or both
        Dictionary<int, int> foodTraits = new();
        foodTraits.Add((int)FoodTraits.DIET, Random.Range(0, Enum.GetNames(typeof(FoodDiet)).Length));
        traits.Add(Traits.FOOD, foodTraits);
        return this;
    }

    public Dictionary<Traits, Dictionary<int, int>> Build()
    {
        return traits;
    }
}

public enum Traits
{
    MOVEMENT,
    FOOD,
    SLEEPING
}
