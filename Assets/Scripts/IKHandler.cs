using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHandler : MonoBehaviour
{
    public Transform goal;
    public Transform endEffector;

    [SerializeField] public List<IKJoint> joints;

    private float magnitude;

    // Use this for initialization
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        goal.transform.position = new Vector3(endEffector.transform.position.x, goal.transform.position.y,
            goal.transform.position.z);

        magnitude = Vector3.Magnitude(goal.position - endEffector.position);

        foreach (var ikJoint in joints)
        {
            ikJoint.RunIK(goal, endEffector);
        }
    }
}