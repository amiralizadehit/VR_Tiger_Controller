using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleMapper : MonoBehaviour
{

    public List<HingeJoint> Hinges;
    public AngleReturn angleObj;

    private float[] angles;


	void Start ()
    {
        

    }
	
	
	void Update ()
    {
        angles = angleObj.GetAngles();
        for (int i = 0; i < angles.Length; i++)
        {
            var jointSpring = new JointSpring()
            {
                spring = Hinges[i].spring.spring,
                damper = Hinges[i].spring.damper,
                targetPosition = angles[i]
            };
            Hinges[i].spring = jointSpring;
        }
    }
}
