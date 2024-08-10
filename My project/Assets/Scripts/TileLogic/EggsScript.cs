using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggsScript : FoodScript
{
    private int energy = 0;
    private int energyPerDay = 24;
    private float chargeRate;
    private float chargeTimer = 0;
    CreatureData parent1;
    CreatureData parent2;

    private void Start()
    {
        chargeRate = TimeManager.Instance.secondsPerDay / energyPerDay;
    }

    public void LayEggs(int numOfEggs, int energy, CreatureData parent1, CreatureData parent2)
    {
        numOfFood = numOfEggs;
        this.energy = energy;
        this.parent1 = parent1;
        this.parent2 = parent2;
    }

    public override int EatFood()
    {
        if (numOfFood - 1 == 0)
        {
            Destroy(this);
        }
        if (numOfFood > 0)
        {
            numOfFood -= 1;
            return energyPerFood;
        }
        return 0;
    }

    private void Update()
    {
        if(Time.time > chargeTimer + chargeRate)
        {
            chargeTimer = Time.time;
            energy += 1;
            if(energy == 100)
            {
                CreateCreature.instance.BreedNewCreature(parent1, parent2);
            }
        }
    }

}
