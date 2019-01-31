using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalMapper : MonoBehaviour {

    public VRInput input;

    public Transform endEffector;

    public TigerRoot tigerRoot;

    public string key;

    private Vector3 initPosition;
    // Use this for initialization
    void Start () {
        initPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateGoal();
    }
    private void UpdateGoal()
    {
        if (input != null)
        {


            Vector3 translate = input.GetTranslate(key);

            var offset = tigerRoot.GetRootTransform().z;

            if (translate != Vector3.zero)
            {
                translate *= 1.5f;
               transform.position = new Vector3(endEffector.position.x, initPosition.y + translate.y, initPosition.z + translate.x + offset);


                //goal.transform.Translate(translate);
            }

        }


    }
}
