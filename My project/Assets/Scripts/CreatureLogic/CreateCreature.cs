using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEditor;
using UnityEditor.SceneTemplate;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class CreateCreature : MonoBehaviour
{

    public static CreateCreature instance;
    [SerializeField] private Tilemap worldMap;
    private BoundsInt mapBorder;
    [SerializeField] GameObject creaturePrefab;
    [SerializeField] int numOfStartingCreatures;

    void Start(){
        instance = this;
        id = 2;
        mapBorder = worldMap.cellBounds;
        for(int i = 0; i < numOfStartingCreatures; i++)
        {
            SpawnCreature();
        }
    }
    private int id;

    public GameObject creatureHolder;

    //at x position
    public void SpawnCreature(){
        id++;
        int randomx = Random.Range(mapBorder.xMin, mapBorder.xMax);
        int randomy = Random.Range(mapBorder.yMin, mapBorder.yMax);
        Vector3 random_position = GameManager.Instance.getGrid().CellToWorld(new Vector3Int(randomx, randomy));
            
        GameObject creature = Instantiate(creaturePrefab, random_position, Quaternion.identity);
        creature.transform.parent = creatureHolder.transform;

        CreatureData data = new(id, 100, Random.Range(UnitUtilities.TILE, UnitUtilities.TILE * 5f), 8, new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
        BaseCreature baseCreature = creature.GetComponent<BaseCreature>();
        baseCreature.SetActions(CreateActions(creature.GetComponent<Rigidbody2D>(), data, creature.GetComponentInChildren<RangeScanner>()));
        
        baseCreature.SetData(data);
        SpriteRenderer spriteR = creature.GetComponent<SpriteRenderer>();
        spriteR.color = data.attributesData.color;
        //Instantiate(creature);
        //Debug.Log("Creature Created");

        creature.name = baseCreature.data.attributesData.ID.ToString();
    }

    public void BreedNewCreature(CreatureData data1, CreatureData data2){
        id++;

        GameObject newCreature = Instantiate(creaturePrefab, creatureHolder.transform);
        BaseCreature creatureBase = newCreature.GetComponent<BaseCreature>();

        CreatureData data3 = CreateData(data1, data2, newCreature.GetComponent<Rigidbody2D>(), newCreature.GetComponentInChildren<RangeScanner>());
        creatureBase.SetData(data3);

        creatureBase.SetActions(CreateActions(newCreature.GetComponent<Rigidbody2D>(),data3,newCreature.GetComponentInChildren<RangeScanner>()));
 
        SpriteRenderer spriteR = newCreature.GetComponent<SpriteRenderer>();
        spriteR.color = data3.attributesData.color;
        newCreature.name = creatureBase.data.attributesData.ID.ToString();
    }

    private CreatureData CreateData(CreatureData parent1, CreatureData parent2, Rigidbody2D creature_rb, RangeScanner scanner){
        CreatureData data;
        int min;
        int max;

        min = parent1.energyData.energy < parent2.energyData.energy ? parent1.energyData.energy : parent2.energyData.energy;
        max = parent1.energyData.energy > parent2.energyData.energy ? parent1.energyData.energy : parent2.energyData.energy;
        int energy = Random.Range(min-1, max + 1);

        min = parent1.attributesData.sightRange < parent2.attributesData.sightRange ? parent1.attributesData.sightRange : parent2.attributesData.sightRange;
        max = parent1.attributesData.sightRange > parent2.attributesData.sightRange ? parent1.attributesData.sightRange : parent2.attributesData.sightRange;
        int sight_range = Random.Range(min -1, max +1);

        float fmin, fmax;
        fmin = parent1.movementData.speed < parent2.movementData.speed ? parent1.movementData.speed : parent2.movementData.speed;
        fmax = parent1.movementData.speed > parent2.movementData.speed ? parent1.movementData.speed : parent2.movementData.speed;
        float speed = Random.Range(fmin - UnitUtilities.TILE/2, fmax + UnitUtilities.TILE/2);

        Color color = Color.Lerp(parent1.attributesData.color, parent2.attributesData.color, 1);
        data = new(id, energy, speed, sight_range, color);
        return data;
    }

    private ActionGraph CreateActions(Rigidbody2D creature_rb, CreatureData data, RangeScanner scanner){
        
        FindFood findFood = new(data.energyData, data.movementData, data.foodData);
        InitAction(findFood, creature_rb, scanner);

        LookForMate lookForMate = new(data.energyData, data.breedData);
        InitAction(lookForMate, creature_rb, scanner);

        Breed breed = new(data);
        InitAction(breed, creature_rb, scanner);

        Wander wander = new(data.energyData, data.movementData);
        InitAction(wander, creature_rb, scanner);

        EatFood eatFood = new(data.energyData, data.foodData);
        InitAction(eatFood, creature_rb, scanner);

        Sleeping sleeping = new();
        InitAction(sleeping, creature_rb, scanner);
        //50% chance of either sleeping during the day or night
        sleeping.SetTraits(Random.Range(0,2) == 0);

        ActionNode findFoodNode = new(findFood);
        ActionNode wanderNode = new(wander);
        ActionNode eatFoodNode = new(eatFood);
        ActionNode lookForMateNode = new(lookForMate);
        ActionNode breedNode = new(breed);
        ActionNode sleepingNode = new(sleeping);

        findFoodNode.AddAction(eatFoodNode);
        findFoodNode.AddAction(wanderNode);

        wanderNode.AddAction(sleepingNode);
        wanderNode.AddAction(breedNode);
        wanderNode.AddAction(lookForMateNode);
        wanderNode.AddAction(findFoodNode);
        wanderNode.AddAction(wanderNode);

        eatFoodNode.AddAction(wanderNode);

        lookForMateNode.AddAction(wanderNode);
        lookForMateNode.AddAction(breedNode);

        breedNode.AddAction(wanderNode);

        sleepingNode.AddAction(wanderNode);

        List<ActionNode> action_list = new();
        action_list.Add(wanderNode);
        action_list.Add(findFoodNode);
        action_list.Add(eatFoodNode);
        action_list.Add(lookForMateNode);
        action_list.Add(breedNode);
        action_list.Add(sleepingNode);
        ActionGraph actions = new(wanderNode, action_list);

        return actions;
    }

    private void InitAction(IAction action, Rigidbody2D creature_rb, RangeScanner scanner){
        action.SetRigidBody(creature_rb);
        action.SetScanner(scanner);
    }
}
