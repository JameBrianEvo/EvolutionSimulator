using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFood : IAction
{
    //Class Variables
    public Rigidbody2D rb;
    private bool finishedEating = false;
    private EnergyData energyData;
    private FoodData foodData;

    public EatFood(EnergyData energyData, FoodData foodData)
    {
        this.energyData = energyData;
        this.foodData = foodData;
    }
    public void AddTraits(TraitData traitData)
    {
        
    }
    public bool EndCondition()
    {
        return foodData.targetFood == null || finishedEating;
    }

    //do nothing
    public void OnExit()
    {
    }

    public void Run()
    {
        //if eating animation finishes, finished eating = true;
        energyData.IncreaseEnergy(foodData.targetFood.EatFood());
        finishedEating = true;
    }

    public bool StartCondition()
    {
        return foodData.targetFood != null;
    }

    public void OnEnter()
    {
        finishedEating = false;
        //start eating animation
    }

    
    //nothing to print
    public void PrintStatus()
    {
    }

    public void SetRigidBody(Rigidbody2D rb)
    {
        this.rb = rb;
    }

    //not needed
    public void SetScanner(RangeScanner scanner)
    {
    }
   
}
