using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int numOfFood = 1;

    [SerializeField]
    public int maxFood = 3;

    [SerializeField]
    public int energyPerFood;

    [SerializeField]
    private bool plant;

    public virtual int EatFood()
    {
        if(numOfFood > 0)
        {
            numOfFood -= 1;
            return energyPerFood;
        }
        return 0;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public bool IsPlant()
    {
        return plant;
    }
}
