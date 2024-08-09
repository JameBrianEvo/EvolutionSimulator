using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class CreatureManager : MonoBehaviour
{
    private float LogTimer = 0;
    [SerializeField]
    private float LogRate = 60f;

    public static CreatureManager instance;

    private string FileName = "/Log.txt";

    [SerializeField]
    public TextAsset log;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        string path = Application.dataPath + FileName;
        StreamWriter writer = new(path, false);
        writer.Write("");
        writer.Close();
    }

    void FixedUpdate(){
        if(LogTimer < LogRate){
            LogTimer += Time.deltaTime;
        }else{
            Debug.Log("Logging State");
            LogTimer = 0;
            LogCreatureStates();
        }
    }

    private void LogCreatureStates(){
        string path = Application.dataPath + FileName;
        StreamReader reader = new(path, false);
        string content = reader.ReadToEnd();
        reader.Close();

        StreamWriter writer = new(path, false);
        List<BaseCreature> listOfCreatures = new List<BaseCreature>(gameObject.GetComponentsInChildren<BaseCreature>());

        writer.Write(content);
        writer.Write("Number of creatures: " + listOfCreatures.Count);
        writer.Write(GetAverageMetaData(listOfCreatures));
        writer.Write(GetAverageTrait(listOfCreatures, Traits.FOOD, FoodTraits.DIET));
        writer.Close();
        
    }

    private string GetAverageMetaData(List<BaseCreature> creatures)
    {
        string averages = "";
        float avSpeed = 0, avRange = 0, avCurEngergy = 0;
        foreach (BaseCreature creature in creatures)
        {
            avRange += creature.data.attributesData.sightRange;
            avSpeed += creature.data.movementData.speed;
            avCurEngergy += creature.data.energyData.currentEnergy;
        }
        avSpeed /= creatures.Count;
        avRange /= creatures.Count;
        avCurEngergy /= creatures.Count;
        averages += "\nAverage Speed of a creature: " + avSpeed;
        averages += "\nAverage Range of a creature: " + avRange;
        averages += "\nAverage Energy of a creature: " + avCurEngergy;
        averages += "\n";
        return averages;
    }

    private string GetAverageTrait(List<BaseCreature> creatures, Traits category, int trait)
    {
        string averages;
        int common;
        int count = 0;
        common = creatures[0].data.traitData.traits[category][trait] & (int)Traits.SUBMASK;
        for (int i = 0; i < creatures.Count; i++)
        {
            //Debug.Log("Checking Trait: " +(creatures[i].data.traitData.traits[category][trait] & (int)Traits.SUBMASK));
            if((common & (int)Traits.SUBMASK) == (creatures[i].data.traitData.traits[category][trait] & (int)Traits.SUBMASK))
            {
                count += 1 ;
            }
            else
            {
                count -= 1;
                if(count < 0)
                {
                    count = 0;
                    common = creatures[i].data.traitData.traits[category][trait] & (int)Traits.SUBMASK;
                }
            }
        }
        averages = "Most common " + category + " " + trait + " is " + common;
        averages += "\n";
        return averages;
    }
}
