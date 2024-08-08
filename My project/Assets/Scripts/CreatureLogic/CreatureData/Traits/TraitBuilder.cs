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
        Dictionary<int,int > movementTraits = new();
        movementTraits.Add(MovementTraits.WATER,RandomTrait(MovementTraits.NUMWATER));
        traits.Add(Traits.MOVEMENT, movementTraits);
        return this;
    }

    public TraitBuilder BuildSleepingTraits()
    {
        Dictionary<int, int> sleepingTraits = new();
        sleepingTraits.Add(SleepTraits.SLEEPPATTERN, RandomTrait(SleepTraits.NUMSLEEPPATTERN));
        traits.Add(Traits.SLEEPING, sleepingTraits);
        return this;
    }

    public TraitBuilder BuildFoodTraits()
    {
        Dictionary<int, int> foodTraits = new();
        foodTraits.Add(FoodTraits.DIET, RandomTrait(FoodTraits.NUMDIET));
        traits.Add(Traits.FOOD, foodTraits);
        return this;
    }

    public Dictionary<Traits, Dictionary<int, int>> Build()
    {
        return traits;
    }

    public static int RandomTrait(int range)
    {
        return Random.Range(0, range) << 8;
    }
}

public enum Traits
{
    SUBMASK = 0xFF00,
    MOVEMENT,
    FOOD,
    SLEEPING
}
