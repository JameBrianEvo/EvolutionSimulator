using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FindFoodDebugger : MonoBehaviour
{
    [SerializeField]
    CreatureData data;
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    RangeScanner scanner;
    IAction findFood;
    // Start is called before the first frame update
    void Start()
    {
        CreatureData data = new(1, 100, Random.Range(30, 40), 8, new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
        findFood = new FindFood(data.energyData, data.movementData, data.foodData);
        findFood.SetRigidBody(rb);
        findFood.SetScanner(scanner);
        scanner.SetRange(data.attributesData.sightRange);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
