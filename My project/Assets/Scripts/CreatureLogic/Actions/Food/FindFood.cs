using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class FindFood : IAction
{
    //trait variables
    FindFoodStrategy findFoodStrategy;

    //action variables
    public Rigidbody2D rb;
    public CreatureData data;
    private RangeScanner scanner;
    private Grid grid;
    private FoodScript food;
    private bool forceQuit;

    //set a food strategy
    //set grid
    public FindFood(bool plant, bool meat)
    {
        if (!plant)
        {
            Debug.Log("MEAT EATER");
            findFoodStrategy = new FindFoodCarnivore();
        } else if (!meat)
        {
            Debug.Log("Plant Eater");
            findFoodStrategy = new FindFoodHerbivore();
        }
        else
        {
            Debug.Log("Both Eater");
            findFoodStrategy = new FindFoodOmnivore();
        }

        grid = GameManager.Instance.getGrid();
    }

    public void SetRigidBody(Rigidbody2D rb)
    {
        this.rb = rb;
    }

    public void SetData(CreatureData data)
    {
        this.data = data;
    }

    public void SetScanner(RangeScanner rangeScanner)
    {
        scanner = rangeScanner;
    }

    //check if creature is already full
    //otherwise the specified food strategy
    public bool StartCondition()
    {
       
        if (data.IsFull())
        {
            return false;
        }
        food = findFoodStrategy.FindFood(scanner, rb);  
        return food != null;
    }


    public void OnEnter()
    {
        rb.velocity = new Vector2(food.GetPosition().x - rb.position.x, food.GetPosition().y - rb.position.y).normalized * data.Speed;
        data.SetNewTargetLocation(grid.WorldToCell(food.GetPosition()));
        forceQuit = false;
    }

    //when running, check for obstacles, if true then "force quit" the action
    public void Run()
    {
        if (ActionUtils.IsObstacleDetected(rb))
        {
            forceQuit = true;
        }
    }

    //if force quit then return true
    //if arrived at food, then return true
    //otherwise return false
    public bool EndCondition(){ 
        return (
           food == null ||
           forceQuit ||
           Vector3.Distance(food.GetPosition(), rb.position) < UnitUtilities.TILE / 2
        );
    }

    //set velocity to 0
    public void OnExit(){
        rb.velocity *= 0;
    }

    override
    public string ToString()
    {
        return "Finding Food";
    }

    public void PrintStatus()
    {
        Debug.Log(ToString());
        Debug.Log("Food Location: " + grid.WorldToCell(food.GetPosition()));
    }
}
