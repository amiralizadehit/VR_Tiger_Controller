using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sadas : MonoBehaviour {

    public HingeJoint hing;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(hing.angle);
	}
}
