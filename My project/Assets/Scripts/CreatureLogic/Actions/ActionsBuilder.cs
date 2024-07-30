using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsBuilder : MonoBehaviour
{
    private List<ActionNode> actionNodes;
    private Rigidbody2D rb;
    private CreatureData data;
    private RangeScanner scanner;

    public ActionsBuilder(Rigidbody2D rb, CreatureData data, RangeScanner scanner)
    {
        this.rb = rb;
        this.data = data;
        this.scanner = scanner;
        actionNodes = new();
    }

    public ActionsBuilder AddWandering()
    {
        Wander wander = new(data.energyData, data.movementData);
        InitAction(wander, rb, scanner);
        actionNodes.Add(new(wander));
        return this;
    }

    public ActionsBuilder AddFindFood()
    {
        FindFood findFood = new(data.energyData, data.movementData, data.foodData);
        InitAction(findFood, rb, scanner);
        actionNodes.Add(new(findFood));
        return this;
    }

    public ActionsBuilder AddEatFood()
    {
        EatFood eatFood = new(data.energyData, data.foodData);
        InitAction(eatFood, rb, scanner);
        actionNodes.Add(new(eatFood));
        return this;
    }

    public ActionsBuilder AddLookForMate()
    {
        LookForMate lookForMate = new(data.energyData, data.breedData);
        InitAction(lookForMate, rb, scanner);
        actionNodes.Add(new(lookForMate));
        return this;
    }

    public ActionsBuilder AddBreed()
    {
        Breed breed = new(data, data.breedData, data.movementData);
        InitAction(breed, rb, scanner);
        actionNodes.Add(new(breed));
        return this;
    }

    public ActionGraph Build()
    {
        ActionNode wanderNode = actionNodes.Find(node => node.action is Wander);

        return new ActionGraph(wanderNode, actionNodes);
    }

    private void InitAction(IAction action, Rigidbody2D creature_rb, RangeScanner scanner)
    {
        action.SetRigidBody(creature_rb);
        action.SetScanner(scanner);
    }
}