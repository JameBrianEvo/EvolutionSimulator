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
    private RangeScanner scanner;
    private Grid grid;
    private FoodData foodData;
    private EnergyData energyData;
    private MovementData movementData;
    private Vector2 startingPosition;

    //set a food strategy
    //set grid
    public FindFood(EnergyData energyData, MovementData movementData, FoodData foodData)
    {
        this.energyData = energyData;
        this.movementData = movementData;
        this.foodData = foodData;
        if (!foodData.eatPlants)
        {
            Debug.Log("MEAT EATER");
            findFoodStrategy = new FindFoodCarnivore();
        } else if (!foodData.eatMeat)
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

    public void SetScanner(RangeScanner rangeScanner)
    {
        scanner = rangeScanner;
    }

    //check if creature is already full
    //otherwise the specified food strategy
    public bool StartCondition()
    {
       
        if (energyData.IsFull())
        {
            return false;
        }
        foodData.targetFood = findFoodStrategy.FindFood(scanner, rb);  
        return foodData.targetFood != null;
    }


    public void OnEnter()
    {
        startingPosition = rb.position;
        rb.velocity = new Vector2(foodData.targetFood.GetPosition().x - rb.position.x, foodData.targetFood.GetPosition().y - rb.position.y).normalized * movementData.speed;
    }

    //do nothing
    public void Run()
    {
    }

    //if force quit then return true
    //if arrived at food, then return true
    //otherwise return false
    public bool EndCondition(){ 
        return (
           foodData.targetFood == null ||
           ActionUtils.IsObstacleDetected(rb) ||
           Vector3.Distance(foodData.targetFood.GetPosition(), rb.position) < UnitUtilities.TILE / 2
        );
    }

    //set velocity to 0
    public void OnExit(){
        rb.velocity *= 0;
        energyData.DecreaseEnergy(ActionUtils.CalculateEnergy(startingPosition, rb.position, movementData.speed));
    }

    override
    public string ToString()
    {
        return "Finding Food";
    }

    public void PrintStatus()
    {
        Debug.Log(ToString());
        Debug.Log("Food Location: " + grid.WorldToCell(foodData.targetFood.GetPosition()));
    }
}
