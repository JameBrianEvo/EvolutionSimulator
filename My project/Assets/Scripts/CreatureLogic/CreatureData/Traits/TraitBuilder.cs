using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
//Use of this class should be only for unique traits
//Traits such as cannibalism are put in FoodData and not This
public class TraitBuilder
{
    //75 is a rough calculation of 30% of 256
    private static int defaultWeight = 75;
    private static float geneticRate = .75f;
    private static float maxWeight = 256;
    private static float maxWeightPercent = .90f;
    CreatureData data, parent1, parent2;
    Dictionary<Traits, Dictionary<int, int>> traits;

    public TraitBuilder(CreatureData data, CreatureData parent1, CreatureData parent2)
    {
        this.parent1 = parent1;
        this.parent2 = parent2;
        this.data = data;
        traits = new();
    }


    public TraitBuilder BuildMovementTraits()
    {
        Dictionary<int,int > movementTraits = new();
        if(parent1 == null)
        {
            movementTraits.Add(MovementTraits.WATER, RandomTrait(MovementTraits.NUMWATER));
            traits.Add(Traits.MOVEMENT, movementTraits);
            return this;
        }

        movementTraits.Add(MovementTraits.WATER, GetTraitFromGenetics(Traits.MOVEMENT, MovementTraits.WATER, MovementTraits.NUMWATER));

        traits.Add(Traits.MOVEMENT, movementTraits);


        return this;
    }

    public TraitBuilder BuildSleepingTraits()
    {
        Dictionary<int, int> sleepingTraits = new();
        if(parent1 == null)
        {
            sleepingTraits.Add(SleepTraits.SLEEPPATTERN, RandomTrait(SleepTraits.NUMSLEEPPATTERN));
            traits.Add(Traits.SLEEPING, sleepingTraits);
            return this;
        }

        sleepingTraits.Add(SleepTraits.SLEEPPATTERN, GetTraitFromGenetics(Traits.SLEEPING, SleepTraits.SLEEPPATTERN, SleepTraits.NUMSLEEPPATTERN));
        //Debug.Log("Sleep Trait Added = " + sleepingTraits[SleepTraits.SLEEPPATTERN]);

        traits.Add(Traits.SLEEPING, sleepingTraits);
        return this;
    }

    public TraitBuilder BuildFoodTraits()
    {
        Dictionary<int, int> foodTraits = new();
        if(parent1 == null)
        {
            foodTraits.Add(FoodTraits.DIET, RandomTrait(FoodTraits.NUMDIET));
            traits.Add(Traits.FOOD, foodTraits);
            return this;
        }

        foodTraits.Add(FoodTraits.DIET, GetTraitFromGenetics(Traits.FOOD, FoodTraits.DIET, FoodTraits.NUMDIET));
        //Debug.Log("Diet Trait Added = " + (foodTraits[FoodTraits.DIET] & (int)Traits.SUBMASK) + " " + (foodTraits[FoodTraits.DIET] & (int)Traits.WEIGHTMASK));

        traits.Add(Traits.FOOD, foodTraits);
        
        return this;
    }

    public Dictionary<Traits, Dictionary<int, int>> Build()
    {
        return traits;
    }

    private static int RandomTrait(int range)
    {
        return (Random.Range(0, range) << 8) + defaultWeight;
    }

    private int GetTraitFromGenetics(Traits category, int trait, int numtrait)
    {
        int subtrait1 = parent1.traitData.traits[category][trait];
        int subtrait2 = parent2.traitData.traits[category][trait];

        if ((subtrait1 & (int)Traits.SUBMASK) == (subtrait2 & (int)Traits.SUBMASK))
        {
            //Debug.Log("Parents have same trait");
            float chance;
            //change of trait passing on is equal to both weights  * .75(capped at 90%)
            chance = (subtrait1 & (int)Traits.WEIGHTMASK) + (subtrait2 & (int)Traits.WEIGHTMASK);
            chance *= geneticRate;
            if (Random.Range(0f, 1f) <= chance / maxWeight)
            {
                //Debug.Log("Trait is passed on");
                //Debug.Log("Weight: " + (subtrait1 & (int)Traits.WEIGHTMASK));
                return (subtrait1 & (int)Traits.SUBMASK) + (int)chance & (int)Traits.WEIGHTMASK;
            }
            else
            {
                return RandomTrait(numtrait);
            }
        }
        else
        {
            //Debug.Log("Parents have different traits");
            //Debug.Log("Parent 1 " + subtrait1);
            //Debug.Log("Parent 2 " + subtrait2);
            float random = Random.Range(0f, 1f);
            float chance1 = subtrait1 & (int)Traits.WEIGHTMASK;
            float chance2 = subtrait2 & (int)Traits.WEIGHTMASK;
            if (chance1 + chance2 > maxWeight * maxWeightPercent)
            {
                float total = chance1 + chance2;
                chance1 = (chance1 / total) * maxWeightPercent;
                chance2 = (chance2 / total) * maxWeightPercent;
            }
            else
            {
                chance1 = chance1 / maxWeight;
                chance2 = chance2 / maxWeight;
            }
            if (random < chance1)
            {
                //Debug.Log("Trait 1 is passed on");
                //Debug.Log("Weight: " + (subtrait1 & (int)Traits.WEIGHTMASK));
                return (subtrait1 & (int)Traits.SUBMASK) + (subtrait1 & (int)Traits.WEIGHTMASK);
            }
            else if (random < chance1 + chance2)
            {
                //Debug.Log("Trait 2 is passed on");
                //Debug.Log("Weight: " + (subtrait2 & (int)Traits.WEIGHTMASK));
                return (subtrait2 & (int)Traits.SUBMASK) + (subtrait2 & (int)Traits.WEIGHTMASK);
            }
            else
            {
                return RandomTrait(numtrait);
            }
        }
    }
}

public enum Traits
{
    SUBMASK = 0xFF00,
    WEIGHTMASK = 0x00FF,
    MOVEMENT,
    FOOD,
    SLEEPING
}
