using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionGraph
{

    public ActionNode root{get; private set;}
    public List<ActionNode> actionNodes { get; private set; }
    public ActionGraph(ActionNode root, List<ActionNode> actionNodes)
    {
        this.root = root;
        this.actionNodes = actionNodes;
    }
}
