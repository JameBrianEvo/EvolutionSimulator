using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MovementData
{
    public float speed { get; private set; }

    public MovementData (float speed)
    {
        this.speed = speed;
    }
}
