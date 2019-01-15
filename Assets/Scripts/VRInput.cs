using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class VRInput : MonoBehaviour
{

    public GameObject RightHand;
    public GameObject LeftHand;
    public GameObject RightFoot;
    public GameObject LeftFoot;


    public Text RightHandText;
    public Text LeftHandText;
    public Text RightFootText;
    public Text LeftFootText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		RightHandText.text =
            $"({RightHand.transform.position.x:0.###}, {RightHand.transform.position.y:0.###}, {RightHand.transform.position.z:0.###})";
        LeftHandText.text =
            $"({LeftHand.transform.position.x:0.###}, {LeftHand.transform.position.y:0.###}, {LeftHand.transform.position.z:0.###})";
        RightFootText.text =
            $"({RightFoot.transform.position.x:0.###}, {RightFoot.transform.position.y:0.###}, {RightFoot.transform.position.z:0.###})";
        LeftFootText.text =
            $"({LeftFoot.transform.position.x:0.###}, {LeftFoot.transform.position.y:0.###}, {LeftFoot.transform.position.z:0.###})";
    }
}
