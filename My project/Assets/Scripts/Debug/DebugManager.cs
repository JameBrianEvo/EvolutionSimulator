using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
    public static DebugManager Instance;
    private BaseCreature creature;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private TMP_Text tId, tAge, tEnergy, tSpeed, tSight, tAction, tLocation;
    [SerializeField]
    CameraControl camcon;
    private bool infoOpened = false;
    // Start is called before the first frame update
    //Hello Testing Testing
    void Start()
    {
        Instance = this;
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.enabled = false;
            camcon.StopFollow();
        }
    }

    private void FixedUpdate()
    {
        if(creature == null)
        {
            canvas.enabled = false;
            return;
        }
        if(infoOpened) {
            SetTextInfo();
        }
    }

    public void Display(BaseCreature _creature)
    {
        infoOpened = true;
        this.creature = _creature;
        canvas.enabled = true;
        camcon.SetFollow(_creature.GetTransform());
    }

    private void SetTextInfo()
    {
        tId.text = creature.data.attributesData.ID.ToString();
        tEnergy.text = creature.data.energyData.currentEnergy.ToString("0,00") + " / " + creature.data.energyData.energy.ToString("0.00");
        tAge.text = creature.GetAge() + "";
        tSight.text = creature.data.attributesData.sightRange.ToString();
        tSpeed.text = creature.data.movementData.speed + "";
        tLocation.text = creature.transform.position + "";
        tAction.text = creature.currentActionNode.action.ToString();
    }
}
