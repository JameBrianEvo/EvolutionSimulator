using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class Wander : IAction
{
    Rigidbody2D rb;
    Vector3Int wanderTarget;
    Grid grid;
    EnergyData energyData;
    MovementData movementData;


    public Wander(EnergyData energyData, MovementData movementData)
    {
        this.movementData = movementData;
        this.energyData = energyData;
        grid = GameManager.Instance.getGrid();
    }

    public bool EndCondition()
    {
        //Debug.Log(forceQuit);
        if (ActionUtils.IsObstacleDetected(rb))
        {
            return true;
        }
        if (Vector3.Distance(rb.position, grid.CellToWorld(wanderTarget)) < UnitUtilities.TILE){
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
        wanderTarget = SetRandomPath();
        Vector3Int gridPosition = GameManager.Instance.getGrid().WorldToCell(rb.position);
        rb.velocity = new Vector2(wanderTarget.x - gridPosition.x, wanderTarget.y - gridPosition.y).normalized * movementData.speed;
        //Debug.Log(wanderTarget);
        //Debug.Log("Speed: " + movementData.speed);
    }

    public void OnExit()
    {
        rb.velocity *= 0;
    }

    public void Run()
    {
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

    public Vector3Int SetRandomPath()
    {
        int negativex = UnityEngine.Random.Range(0f, 1f) > .5f ? -1 : 1;
        int negativey = UnityEngine.Random.Range(0f, 1f) > .5f ? -1 : 1;
        Vector3Int og_position = grid.WorldToCell(rb.position);
        Vector3Int position = grid.WorldToCell(rb.position);

        position.x = og_position.x + negativex * UnityEngine.Random.Range(5, 10);
        position.y = og_position.y + negativey * UnityEngine.Random.Range(5, 10);
        return position;
    }
}
