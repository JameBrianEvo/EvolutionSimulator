using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class Wander : IAction
{
    CreatureData data;
    Rigidbody2D rb;
    bool forceQuit = false;
    Vector3Int wanderTarget;
    Grid grid;


    public Wander()
    {
        grid = GameManager.Instance.getGrid();
    }

    public bool EndCondition()
    {
        //Debug.Log(forceQuit);
        if (forceQuit == true)
        {
            return true;
        }
        if (Vector3.Distance(rb.position, grid.CellToWorld(wander_target)) < UnitUtilities.TILE){
            return true;
        }
        return false;
    }

    /*
     * On enter: 
     * a random target coordinate is set
     * the velocity is set such that it goes towards the coordinate
     */
    public void OnEnter()
    {
        wanderTarget = data.SetRandomPath();
        forceQuit = false;

        Vector3Int gridPosition = GameManager.Instance.getGrid().WorldToCell(rb.position);
        rb.velocity = new Vector2(wanderTarget.x - gridPosition.x, wanderTarget.y - gridPosition.y).normalized * data.Speed;
        Debug.Log(rb.velocity);
    }

    public void OnExit()
    {
        rb.velocity *= 0;
    }

    public void Run()
    {
        if (ActionUtils.IsObstacleDetected(rb))
        {
       
            forceQuit = true;
        }
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
        //scanner is not used in this action
    }

    public bool StartCondition()
    {
        return true;
    }

    override
    public string ToString(){
        return "Wander";
    }

    public void PrintStatus()
    {
        Debug.Log(this.ToString());
        Debug.Log("Target Location: " + wanderTarget.ToString());
        Debug.Log("Distance: " + Vector3.Distance(rb.position, grid.CellToWorld(wanderTarget)));
    }
}
