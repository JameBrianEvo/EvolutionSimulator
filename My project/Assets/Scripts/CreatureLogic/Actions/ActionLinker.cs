using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionLinker
{
    List<ActionNode> actionNodes;
    public ActionLinker(ActionGraph graph)
    {
        actionNodes = graph.actionNodes;
    }

    public ActionLinker LinkWandering()
    {
        ActionNode wanderNode = actionNodes.Find(node => node.action is Wander);
        SafeAddAction(wanderNode, actionNodes.Find(node => node.action is Breed));
        SafeAddAction(wanderNode, actionNodes.Find(node => node.action is LookForMate));
        SafeAddAction(wanderNode, actionNodes.Find(node => node.action is FindFood));
        SafeAddAction(wanderNode, wanderNode);
        return this;
    }

    public ActionLinker LinkFindFood()
    {
        ActionNode findFoodNode = actionNodes.Find(node => node.action is FindFood);
        SafeAddAction(findFoodNode,actionNodes.Find(node => node.action is EatFood));
        SafeAddAction(findFoodNode, actionNodes.Find(node => node.action is Wander));
        return this;
    }

    public ActionLinker LinkEatFood()
    {
        ActionNode eatFoodNode = actionNodes.Find(node => node.action is EatFood);
        SafeAddAction(eatFoodNode, actionNodes.Find(node => node.action is Wander));
        return this;
    }

    public ActionLinker LinkLookForMate()
    {
        ActionNode lookForMateNode = actionNodes.Find(node => node.action is LookForMate);
        SafeAddAction(lookForMateNode, actionNodes.Find(node => node.action is Breed));
        SafeAddAction(lookForMateNode, actionNodes.Find(node => node.action is Wander));
        return this;
    }

    public ActionLinker LinkBreed()
    {
        ActionNode breedNode = actionNodes.Find(node => node.action is Breed);
        SafeAddAction(breedNode, actionNodes.Find(node => node.action is Wander));
        return this;
    }

    private void SafeAddAction(ActionNode root, ActionNode addition)
    {
        if(addition != null)
        {
            root.AddAction(addition);
        }
    }
}
