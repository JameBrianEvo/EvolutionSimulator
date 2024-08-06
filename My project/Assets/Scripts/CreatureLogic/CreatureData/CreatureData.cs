using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
[Serializable]

public class CreatureData
{
    public EnergyData energyData { get; }
    public AttributesData attributesData { get; }
    public MovementData movementData { get; }
    public BreedData breedData { get; }
    public FoodData foodData { get; }
    public TraitData traitData { get; private set; }

    public CreatureData(int ID, int energy, float speed, int sightRange, Color color)
    {
        //Debug.Log("Speed During Creation:" + speed);
        movementData = new(speed);
        attributesData = new(ID,sightRange,color);
        energyData = new(energy);
        breedData = new();
        foodData = new();
    }

    public void SetTraits(Dictionary<Traits, Dictionary<int, int>> traits)
    {
        traitData = new(traits);
    }
}
