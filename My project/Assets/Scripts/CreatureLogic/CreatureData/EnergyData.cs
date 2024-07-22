using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyData
{
    public int energy { get; private set; }
    public int currentEnergy { get; private set; }

    public EnergyData(int Energy)
    {
        this.energy = Energy;
        currentEnergy = Energy / 2;
    }

    public void DecreaseEnergy(int amount)
    {
        //Debug.Log("Decreasing Energy" + ID);
        currentEnergy -= amount;
    }

    public void IncreaseEnergy(int amount)
    {
        currentEnergy += amount;
        if (currentEnergy > energy)
        {
            currentEnergy = energy;
        }
    }

    //returns a bool based on if the energy capacity is full or not
    public bool IsFull()
    {
        return currentEnergy >= energy;
    }
}
