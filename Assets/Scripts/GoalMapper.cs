using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalMapper : MonoBehaviour {

    public VRInput input;

    public Transform endEffector;

    public TigerRoot tigerRoot;

    public string key;

    public Vector3 scaleDown = new Vector3(1,-1,1);

    private Vector3 old_pos = Vector3.zero;

    private Vector3 initPosition;
    // Use this for initialization
    void Start ()
    {
        //transform.position = endEffector.position;
        initPosition = endEffector.position - tigerRoot.GetRootTransform(); //transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateGoal();
    }


    private void UpdateGoal()
    {
        Vector3 translate = Vector3.zero;

        if (input != null)
        {
            translate = input.GetTranslate(key);
            translate.x = 0;
            translate.y *= -1;
        }
       
        
        transform.position = tigerRoot.GetRootTransform() + initPosition + translate;

       


        //if (input != null)
        //{


        //    Vector3 translate = input.GetTranslate(key);
        //    var offset = tigerRoot.GetRootTransform();
        //    old_pos = endEffector.position;


        //    if (translate != Vector3.zero)
        //    {
        //        translate *= 1.5f;
        //        if (key == "rHand")
        //            Debug.Log(translate);
        //        Vector3 npos = 0.5f * old_pos + 0.5f * new Vector3(endEffector.position.x, initPosition.y + translate.y, initPosition.z + translate.z+offset.z);
        //        npos.x = endEffector.position.x;
        //        transform.position = new Vector3(endEffector.position.x, initPosition.y + translate.y*scaleDown+ endEffector.position.y , initPosition.z + translate.x * scaleDown + offset.z );


        //        //goal.transform.Translate(translate);
        //    }

        //}


    }
}
