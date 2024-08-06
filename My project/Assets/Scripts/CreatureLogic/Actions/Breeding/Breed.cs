using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breed : IAction
{
    //class variables
    BreedData breedData;
    MovementData movementData;
    EnergyData energyData;
    CreatureData data;
    RangeScanner scanner;
    Rigidbody2D rb;
    Vector2 startingPosition;
    float breedDuration;
    float breedTimeEnd;
    float breedCooldown;
    float childChance;

    public void PrintStatus()
    {
        
    }

    public Breed(CreatureData data)
    {
        this.data = data;
        this.breedData = data.breedData;
        this.movementData = data.movementData;
        this.energyData = data.energyData;
        //breed duration lasts a quarter traits may further change these
        breedDuration = TimeManager.Instance.secondsPerDay / 4;
        breedCooldown = TimeManager.Instance.secondsPerDay;
        breedTimeEnd = 0;
        childChance = 0.5f;
    }

    //if on cooldown then return false
    //if it has a target already, then return true
    public bool StartCondition()
    {
        if (Time.time < breedTimeEnd + breedCooldown)
        {
            //Debug.Log("Breed On Coowndown");
            return false;
        }

        if (breedData.targetCreature != null)
        {
            return true;
        }

        foreach (BaseCreature creature in scanner.GetCreatures())
        {
            string currentAction = creature.currentActionNode.action.ToString();
            if (creature.data.energyData.currentEnergy >= creature.data.energyData.energy / 2
                || currentAction.Equals("Breed"))
            {
                breedData.targetCreature = creature;
                //Debug.Log("Breeder " + data.ID + " |Breed Victim" + target.data.ID);
                return true;
            }
            }
        return false;
    }

    //Set time limit of breed
    public void OnEnter()
    {
        startingPosition = rb.position;
        breedTimeEnd = Time.time + breedDuration;
        Vector2 targetPosition = breedData.targetCreature.transform.position;
        rb.velocity = new Vector2(targetPosition.x - rb.position.x, targetPosition.y - rb.position.y).normalized * movementData.speed;
    }

    //do nothing
    public void Run()
    {
    }

    //if creature in range is in range and has started the breed action
    //if breed timer ends
    public bool EndCondition()
    {
        if(Time.time > breedTimeEnd)
        {
            return true;
        }
        return (Vector3.Distance(breedData.targetCreature.transform.position, rb.position) < UnitUtilities.TILE);
    }

    //if breeding was successful, then
    //create a baby
    //decrease energy
    public void OnExit()
    {
        if(Random.Range(0f,1f) > childChance && Time.time < breedTimeEnd)
        {
            //Debug.Log("Successful Breeding " + data.ID + " |Breed Victim" + target.data.ID);
            CreateCreature.instance.BreedNewCreature(breedData.targetCreature.data, data);
        }
        energyData.DecreaseEnergy(ActionUtils.CalculateEnergy(startingPosition, rb.position, movementData.speed));
        breedData.targetCreature = null;
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

    public void AddTraits(TraitData data)
    {
        
    }
}
