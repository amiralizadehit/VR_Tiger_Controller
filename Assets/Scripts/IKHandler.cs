using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHandler : MonoBehaviour
{
    public Transform goal;
    public Transform endEffector;

    public List<IKJoint> joints;

	// Use this for initialization
	void Awake ()
    {
        foreach (var ikJoint in joints)
        {
            ikJoint.SetEffector(endEffector);
            ikJoint.SetGoal(goal);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        goal.transform.position = new Vector3(endEffector.transform.position.x, goal.transform.position.y, goal.transform.position.z);

        foreach (var ikJoint in joints)
        {
            ikJoint.SetEffector(endEffector);
            ikJoint.SetGoal(goal);
        }
    }
}
