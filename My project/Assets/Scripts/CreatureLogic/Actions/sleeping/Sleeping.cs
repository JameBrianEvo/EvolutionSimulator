using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleeping : IAction
{
    //traits
    bool nightSleeper;

    //class variables
    private float sleepStartTime;
    private float sleepDuration;

    public Sleeping()
    {
        nightSleeper = true;
        sleepDuration = TimeManager.Instance.secondsPerDay - TimeManager.Instance.daytimeSeconds;
    }

    public void AddTraits(TraitData traitData)
    {
        switch (traitData.traits[Traits.SLEEPING][(int)SleepTraits.SLEEPPATTERN])
        {
            case (int)SleepPattern.DAY:
                sleepDuration = TimeManager.Instance.secondsPerDay;
                break;
            case (int)SleepPattern.NIGHT:
                sleepDuration = TimeManager.Instance.secondsPerDay - TimeManager.Instance.daytimeSeconds;
                break;
        }
    }

    public bool StartCondition()
    {
        return TimeManager.Instance.IsDay != nightSleeper;
    }

    public void OnEnter()
    {
        sleepStartTime = Time.time;
    }

    public void Run()
    {
        //do nothing
    }

    public bool EndCondition()
    {
        
        return Time.time > (sleepStartTime + sleepDuration);

    }   

    public void OnExit()
    {
        //do nothing
    }

    public void PrintStatus()
    {
        Debug.Log("Sleeping");
        Debug.Log(Time.time + ": Current Time");
        Debug.Log(sleepStartTime + sleepDuration + ": End Time");
    }

    public void SetRigidBody(Rigidbody2D rb)
    {
        //not needed
    }

    public void SetScanner(RangeScanner scanner)
    {
        //not needed (for now)
    }

    
}
