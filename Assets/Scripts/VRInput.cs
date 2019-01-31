using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VRInput : MonoBehaviour
{

    public SteamVR_Action_Boolean ReadyAction;

    public GameObject RightHand;
    public GameObject LeftHand;
    public GameObject RightFoot;
    public GameObject LeftFoot;


    public Text RightHandText;
    public Text LeftHandText;
    public Text RightFootText;
    public Text LeftFootText;

    private Hand lHand;
    private Hand rHand;
    private bool isCalibrated;

    private Dictionary<string, Vector3> initPositions;

	// Use this for initialization
	void Start ()
    {
        

        initPositions = new Dictionary<string, Vector3>(4);

    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateUI();

    }

    private void OnEnable()
    {
        lHand = LeftHand.GetComponent<Hand>();
        rHand = RightHand.GetComponent<Hand>();


        if (ReadyAction!=null)
        {
            
            ReadyAction.AddOnChangeListener(OnPressButton, rHand.handType);
            ReadyAction.AddOnChangeListener(OnPressButton, lHand.handType);
        }
    }

    private void OnDisable()
    {
        if (ReadyAction != null)
        {
            ReadyAction.RemoveOnChangeListener(OnPressButton, rHand.handType);
            ReadyAction.RemoveOnChangeListener(OnPressButton, lHand.handType);
        }
            
    }

    private void OnPressButton(SteamVR_Action_In actionIn)
    {
        
        if (ReadyAction.GetStateDown(rHand.handType)|| ReadyAction.GetStateDown(lHand.handType))
        {
            Init();
        }
    }

    private void Init()
    {

        if (!isCalibrated)
        {
            initPositions.Add("rHand",RightHand.transform.position);
            initPositions.Add("lHand",LeftHand.transform.position);
            initPositions.Add("rFoot",RightFoot.transform.position);
            initPositions.Add("lFoot",LeftFoot.transform.position);
            isCalibrated = true;
        }
        print("Calibrated");
    }

    private void UpdateUI()
    {
        RightHandText.text =
            $"({RightHand.transform.position.x:0.###}, {RightHand.transform.position.y:0.###}, {RightHand.transform.position.z:0.###})";
        LeftHandText.text =
            $"({LeftHand.transform.position.x:0.###}, {LeftHand.transform.position.y:0.###}, {LeftHand.transform.position.z:0.###})";
        RightFootText.text =
            $"({RightFoot.transform.position.x:0.###}, {RightFoot.transform.position.y:0.###}, {RightFoot.transform.position.z:0.###})";
        LeftFootText.text =
            $"({LeftFoot.transform.position.x:0.###}, {LeftFoot.transform.position.y:0.###}, {LeftFoot.transform.position.z:0.###})";
    }

    public Vector3 GetTranslate(string tracker)
    {
        Vector3 result = Vector3.zero;
        if (isCalibrated)
        {
            switch (tracker)
            {

                case "rHand":
                    result= RightHand.transform.position - initPositions[tracker];
                    break;
                case "lHand":
                    result= LeftHand.transform.position - initPositions[tracker];
                    break;
                case "rFoot":
                    result = RightFoot.transform.position - initPositions[tracker];
                    break;
                case "lFoot":
                    result = LeftFoot.transform.position - initPositions[tracker];
                    break;

            }
        }
        return result;
    }
}
