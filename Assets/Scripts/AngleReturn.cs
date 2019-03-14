using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleReturn : MonoBehaviour
{
    // here we should set IK chain all in order, from parent to the last child.
    public List<Transform> Bones;

    public bool isLeg;

    //here we keep track of initial vectors each joint makes with its child in IK chain (except last joint which does not have any child)
    private Vector3[] InitialVectors;

    private float[] Angles;


    // Use this for initialization
    void Awake()
    {
        InitialVectors = new Vector3[Bones.Count - 1];
        Angles = new float[Bones.Count - 1];

        for (int i = 0; i < Bones.Count - 1; i++)
        {
            Vector3 vector = Bones[i + 1].position - Bones[i].position;
            InitialVectors[i] = vector;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Bones.Count - 1; i++)
        {
            //current vector
            Vector3 newVector = Bones[i + 1].position - Bones[i].position;


            var t = (isLeg) ? -1 : 1; //rotation sign is different for legs and hands


            //Here we store the angle which init vector makes with the current vector (after IK update)
            //we should also multiply it by the sign of their cross product in x axis to know when the angle is negative and when it's positive.

            Angles[i] = Vector3.Angle(InitialVectors[i],
                            newVector) * Mathf.Sign(Vector3.Cross(InitialVectors[i], newVector).x) * t;

            //we should also take parent rotation into consideration,
            //hence, for each joint, we should subtract the rotation of all the joints above that to get its own pure rotation.

            float parentValues = 0;
            for (var j = 0; j < i; j++)
            {
                parentValues += Angles[j];
            }

            Angles[i] -= parentValues;
        }


        
    }

    public float[] GetAngles()
    {
        return Angles;
    }
}