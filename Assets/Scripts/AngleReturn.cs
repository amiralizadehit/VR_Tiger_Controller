using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleReturn : MonoBehaviour
{
    public List<Transform> Bones;

    public Transform endEffector;

    //here we keep track of initial vectors each joint makes with its child in IK chain (except last joint which does not have any child)
    private Vector3[] InitialVectors;

    private float[] Angles;


    // Use this for initialization
    void Start()
    {
        InitialVectors = new Vector3[Bones.Count-1];
        Angles = new float[Bones.Count-1];
      
        for (int i = 0; i < Bones.Count - 1; i++)
        {
            print("hello");
            Vector3 vector = Bones[i + 1].position - Bones[i].position;
            InitialVectors[i] = vector;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Bones.Count-1; i++)
        {
            Vector3 newVector = Bones[i + 1].position - Bones[i].position;
            //Vector3 toEF = endEffector.position - Bones[i].position;

            //Here we store the angle which init vector makes with the current vector (after IK update)
            //we should also multiply it by the sign of their cross product in x axis to know when the angle is negative and when it's positive

            Angles[i] = Vector3.Angle(InitialVectors[i],
                newVector) * -Mathf.Sign(Vector3.Cross(InitialVectors[i],newVector).x);
        }

        Debug.Log(Angles[0]);
        
    }
}