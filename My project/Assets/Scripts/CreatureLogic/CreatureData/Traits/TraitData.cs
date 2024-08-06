using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TraitData
{
    public Dictionary<Traits, Dictionary<int, int>> traits;

    public TraitData(Dictionary<Traits, Dictionary<int, int>> traits)
    {
        this.traits = traits;
    }
}
