using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForMate : IAction
{
    //Class Variables
    Rigidbody2D rb;
    RangeScanner scanner;
    EnergyData energyData;
    BreedData breedData;
    int requiredEnergy;
    float cooldown;
    float searchTimeEnd;
    float searchDuration;

    public LookForMate(EnergyData energyData, BreedData breedData)
    {
        this.energyData = energyData;
        this.breedData = breedData;
        float secondsPerDay = TimeManager.Instance.secondsPerDay;
        cooldown = secondsPerDay;
        searchDuration = secondsPerDay / 4;
    }
    public void PrintStatus()
    {
        Debug.Log("End of search and time: " + searchTimeEnd + " " + Time.time);
    }


    public void OnEnter()
    {
        searchTimeEnd = Time.time + searchDuration;
    }

    //has enough energy to mate
    //is not on cooldown
    public bool StartCondition()
    {
        requiredEnergy = energyData.energy / 2;
        if(Time.time < searchTimeEnd + cooldown)
        {
            return false;
        }
        return energyData.currentEnergy >= requiredEnergy;
    }

    //continuously search for mate
    //traits may have the creature also walk around while searching
    //O(n)
    public void Run()
    {
        foreach (BaseCreature creature in scanner.GetCreatures())
        {
            //Debug.Log(creature.data.ID + "In Mating Range");
            if (creature.data.energyData.currentEnergy > creature.data.energyData.energy/2)
            {
                //Debug.Log("Mate Found");
                breedData.targetCreature = creature;
            }
        }
    }

    //When a mate has been found
    //when the timer has run out
    public bool EndCondition()
    {
        if(searchTimeEnd < Time.time)
        {
            return true;
        }

        return breedData.targetCreature != null;
    }

    //reset cooldown
    public void OnExit()
    { 
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
        return "LookForMate";
    }
}
