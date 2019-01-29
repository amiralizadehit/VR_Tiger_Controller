using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class IKHandler : MonoBehaviour
{
    public SteamVR_Action_Boolean plantAction;

    public Hand hand;

    public GameObject prefabToPlant;


    private void OnEnable()
    {
        if (hand == null)
            hand = this.GetComponent<Hand>();

        if (plantAction == null)
        {
            Debug.LogError("No Ready action assigned");
            return;
        }

        plantAction.AddOnChangeListener(OnPlantActionChange, hand.handType);
    }

    private void OnDisable()
    {
        if (plantAction != null)
            plantAction.RemoveOnChangeListener(OnPlantActionChange, hand.handType);
    }

    private void OnPlantActionChange(SteamVR_Action_In actionIn)
    {
        if (plantAction.GetStateDown(hand.handType))
        {
            Plant();
        }
    }

    public void Plant()
    {
        StartCoroutine(DoPlant());
    }

    private IEnumerator DoPlant()
    {
        
    }


}