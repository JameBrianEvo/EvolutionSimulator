using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AttributesData
{
    //things without private set should never with any update change
    public int ID { get; }
    public int sightRange { get; private set; }
    public float timeOfBirth { get; }
    public Color color { get; private set; }

    public AttributesData(int ID, int SightRange, Color color)
    {
        this.ID = ID;
        this.sightRange = SightRange;

        this.color = color;
        timeOfBirth = Time.time;
    }
}
