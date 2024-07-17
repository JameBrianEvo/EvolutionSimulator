using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForMate : IAction
{
    //Class Variables
    CreatureData data;
    Rigidbody2D rb;
    RangeScanner scanner;
    int requiredEnergy;
    //the time when creature can look for mate again
    //ie: time is 1:20 then creature can only look for mate after 1:20
    float nextLookForMate = 0;
    //cooldown for looking for a mate
    float cooldown;
    bool mateFound;
    float searchTimeEnd;
    float searchDuration;

    public LookForMate()
    {
        float secondsPerDay = TimeManager.Instance.secondsPerDay;
        nextLookForMate = Random.Range(0f, secondsPerDay);
        cooldown = secondsPerDay;
        searchDuration = secondsPerDay / 4;
    }
    public void PrintStatus()
    {
        Debug.Log("Next Look For Mate: " + nextLookForMate);
        Debug.Log("End of search and time: " + searchTimeEnd + " " + Time.time);
    }


    public void OnEnter()
    {
        mateFound = false;
        searchTimeEnd = Time.time + searchDuration;
        //Debug.Log("Looking for mate");
    }

    //has enough energy to mate
    //is not on cooldown
    public bool StartCondition()
    {
        requiredEnergy = data.Energy / 2;
        if(Time.time < nextLookForMate)
        {
            return false;
        }
        return data.CurrentEnergy >= requiredEnergy;
    }

    //continuously search for mate
    //traits may have the creature also walk around while searching
    //O(n)
    public void Run()
    {
        foreach (BaseCreature creature in scanner.GetCreatures())
        {
            //Debug.Log(creature.data.ID + "In Mating Range");
            if (creature.data.CurrentEnergy > creature.data.Energy/2)
            {
                //Debug.Log("Mate Found");
                mateFound = true;
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

        return mateFound;
    }

    //reset cooldown
    public void OnExit()
    {
        nextLookForMate = Time.time + cooldown;
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
}
