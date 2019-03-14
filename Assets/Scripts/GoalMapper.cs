using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalMapper : MonoBehaviour {

    public VRInput input;

    public TigerRoot tigerRoot;

    public string key;

    public float tuningParameter = 0.25f;

    public Transform endEffector;


    private Vector3 initPosition;


    void Start ()
    {
        
        //initPosition = transform.localPosition;
        initPosition = tigerRoot.transform.InverseTransformPoint(transform.position);
 
    }


    void Update ()
    {
        UpdateGoal();
    }


    private void UpdateGoal()
    {
        Vector3 VRTranslate = Vector3.zero;

        if (input != null)
        {
            VRTranslate = input.GetTranslate(key);
        }

        // transform.localPosition = initPosition + new Vector3(VRTranslate.z,0,VRTranslate.y);

        /*transform.localPosition = new Vector3
            (initPosition.x-VRTranslate.z,
            initPosition.y,
            (gameObject.tag=="IKGoal")
                ?Mathf.Clamp(initPosition.z+VRTranslate.y, tigerRoot.transform.localPosition.z+tuningParameter,Mathf.Infinity):initPosition.z+VRTranslate.y);*/

        //We want the IK target balls to move relative to the tiger body when the user moves his/her hands and feet relative to his/her body
        //Thus, we compute and clamp the target positions in the tiger's root coordinates
        //in the tiger root coordinates, z-axis points down and x-axis points backward
        Vector3 newPos = new Vector3(initPosition.x - VRTranslate.z, initPosition.y, initPosition.z + VRTranslate.y );

        //Clamp the vertical position so that the end effector does not try to break the joint limits
        newPos.z = Mathf.Clamp(newPos.z, tuningParameter, Mathf.Infinity);
        //Transform back to world space
        newPos = tigerRoot.transform.TransformPoint(newPos);
        transform.position = newPos;
        //Debug.Log(VRTranslate);

    }
}
