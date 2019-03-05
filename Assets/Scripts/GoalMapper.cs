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
        initPosition = transform.position;
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

        Vector3 newPos = new Vector3(transform.position.x,transform.position.y - VRTranslate.y,transform.position.z + VRTranslate.z);

        if (gameObject.tag == "IKGoal")
        {
            if(tigerRoot.transform.position.y>endEffector.position.y+tuningParameter)
                newPos.y = Mathf.Clamp(newPos.y, -Mathf.Infinity, tigerRoot.transform.position.y - tuningParameter);
        }

        transform.position = newPos;
    }
}
