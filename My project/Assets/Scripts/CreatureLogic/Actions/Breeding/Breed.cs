using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breed : IAction
{
    //class variables
    BaseCreature target;
    CreatureData data;
    RangeScanner scanner;
    Rigidbody2D rb;
    float breedDuration;
    float breedTimeEnd;
    float nextBreedTime;
    float breedCooldown;
    bool canBreed;

    public void PrintStatus()
    {
        
    }

    public Breed()
    {
        //breed duration lasts a quarter traits may further change these
        breedDuration = TimeManager.Instance.secondsPerDay / 4;
        breedCooldown = TimeManager.Instance.secondsPerDay;
        breedTimeEnd = 0;
        nextBreedTime = 0;
    }

    //creature in range that has met the conditions of breedable
    //creature in range is in breed action, and creature is willing to breed
    //creature has already bred, then it is unable to breed according to its cooldown
    public bool StartCondition()
    {
        if (Time.time < nextBreedTime)
        {
            //Debug.Log("Breed On Coowndown");
            return false;
        }

        foreach(BaseCreature creature in scanner.GetCreatures())
        {
            string currentAction = creature.currentActionNode.action.ToString();
            if (creature.data.CurrentEnergy >= creature.data.Energy / 2
                || currentAction.Equals("LookForMate")
                || currentAction.Equals("Breed")) 
            {
                target = creature;
                //Debug.Log("Breeder " + data.ID + " |Breed Victim" + target.data.ID);
                return true;
            }
        }
        return false;
    }

    //Set time limit of breed
    public void OnEnter()
    {
        breedTimeEnd = Time.time + breedDuration;
        canBreed = false;
        rb.velocity = new Vector2(target.transform.position.x - rb.position.x, target.transform.position.y - rb.position.y).normalized * data.Speed;
    }

    //if check if the target is going to breed
    //else do nothing
    public void Run()
    {
        if (target.currentActionNode.action.ToString().Equals("Breed"))
        {
            //Debug.Log("Can Breed");
            canBreed = true;
        }
    }

    //if creature in range is in range and has started the breed action
    //if breed timer ends
    public bool EndCondition()
    {
        if(Time.time > breedTimeEnd)
        {
            return true;
        }
        return canBreed && (Vector3.Distance(target.GetTransform().position, rb.position) < UnitUtilities.TILE);
    }

    //if breeding was successful, then
    //create a baby
    //decrease energy
    public void OnExit()
    {
        if(Random.Range(0f,1f) > .5)
        {
            //Debug.Log("Successful Breeding " + data.ID + " |Breed Victim" + target.data.ID);
            CreateCreature.instance.BreedNewCreature(target.data, data);
        }
        //decrease energy
        nextBreedTime = Time.time + breedCooldown;
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

    override
    public string ToString()
    {
        return "Breed";
    }
}
