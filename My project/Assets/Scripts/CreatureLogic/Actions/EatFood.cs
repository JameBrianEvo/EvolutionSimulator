using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFood : IAction
{
    //Class Variables
    public Rigidbody2D rb;
    public CreatureData data;
    private RangeScanner scanner;
    private FoodScript food;
    private bool finishedEating = false;

    public bool EndCondition()
    {
        if(food == null)
        {
            return true;
        }
        if (finishedEating)
        {
            return true;
        }

        return false;
    }

    public void OnExit()
    {

    }

    public void Run()
    {
        //if eating animation finishes, finished eating = true;
        data.IncreaseEnergy(food.EatFood());
        finishedEating = true;
    }

    public bool StartCondition()
    {
        food = scanner.GetNearestFood();
        return food != null;
    }

    public void OnEnter()
    {
        finishedEating = false;
        //start eating animation

    }

    

    public void PrintStatus()
    {
        Debug.Log(food);
    }



    public void SetData(CreatureData data)
    {
        this.data = data;
    }

    public void SetRigidBody(Rigidbody2D rb)
    {
        this.rb = rb;
    }

    public void SetScanner(RangeScanner scanner)
    {
        this.scanner = scanner;
    }
   
}
