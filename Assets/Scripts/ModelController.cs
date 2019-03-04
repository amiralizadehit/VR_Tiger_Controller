using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Networking;

public class ModelController : MonoBehaviour
{
    public float totalVolume;


    public float totalMass;
    public float modelDamper;
    public float modelSpring;

    public bool useCustomValues;

    //public List<Controller> controllers;

    [Header("Body Parts")]
    [NamedArrayAttribute(new string[]
    {
        "Waist (0)",
        "Neck (1)",
        "Right Leg Toe (2)",
        "Right Leg Shin (3)",
        "Right Leg Thigh (4)",
        "Right Hand Toe (5)",
        "Right Hand Forearm (6)",
        "Right Hand Arm (7)",
        "Left Leg Toe (8)",
        "Left Leg Shin (9)",
        "Left Leg Thigh (10)",
        "Left Hand Toe (11)",
        "Left Hand Forearm (12)",
        "Left Hand Arm (13)",
        "Left Leg Upper (14)",
        "Right Leg Upper (15)"
    })]
    public float[] parts;

    [Header("Actions")]
    public bool useActionValues;
    [NamedArrayAttribute(new string[]
    {
        "Moving Hands",
        "Moving Legs",
    })]
    public float[] actions;




    public GameObject root;
    public GameObject[] bones;


    void Awake()
    {
        Time.fixedDeltaTime = 0.001f;
        
        ModelInit();
    }

    void Start()
    {
        CalculateMass();
    }

    void Update()
    {
        UpdateModel();
    }

    private void ModelInit()
    {
        if (parts.Length == 0)
            parts = new float[bones.Length];

        for (int i = 0; i < bones.Length; i++)
        {
            JointSpring spring = new JointSpring()
            {
                spring = (i==0)?modelSpring/2:modelSpring,
                damper = modelDamper,
                targetPosition = (useCustomValues)?parts[i]:bones[i].GetComponent<HingeJoint>().spring.targetPosition
            };
            bones[i].GetComponent<HingeJoint>().spring = spring;

            var capsuleCollider = bones[i].GetComponent<CapsuleCollider>();
            if(capsuleCollider != null) //Capsule Collider
                totalVolume += (float) (Math.PI * Math.Pow(capsuleCollider.radius, 2) *
                                    (4f / 3f * capsuleCollider.radius + capsuleCollider.height));
            else
            { //Box Collider
                var boxCollider = bones[i].GetComponent<BoxCollider>();
                totalVolume += boxCollider.size.x * boxCollider.size.y * boxCollider.size.z;
            }
        }
    }

    private void UpdateModel()
    {
        if (useActionValues)
        {

            parts[6] = actions[0];
            parts[7] = actions[0];
            parts[8] = actions[0];
            parts[11] = actions[0];
            parts[12] = actions[0];
            parts[13] = actions[0];

            parts[2] = actions[0];
            parts[3] = actions[0];
            parts[4] = actions[0];
            parts[15] = actions[0];
            parts[8] = actions[0];
            parts[9] = actions[0];
            parts[10] = actions[0];
            parts[14] = actions[0];

        }
   

        for (int i = 0; i < bones.Length; i++)
        {
            JointSpring spring = new JointSpring()
            {
                spring = (i == 0) ? modelSpring / 2 : modelSpring,
                damper = modelDamper,
                targetPosition = parts[i]
            };
            if(useActionValues)
                bones[i].GetComponent<HingeJoint>().spring = spring;
        }
    }

    private void CalculateMass()
    {
        float radius;
        float height;
        foreach (var t in bones)
        {
            if (t.GetComponent<CapsuleCollider>() != null)
            {
                radius = t.GetComponent<CapsuleCollider>().radius;
                height = t.GetComponent<CapsuleCollider>().height;

                t.GetComponent<Rigidbody>().mass =
                    totalMass * (float)(Math.PI * Math.Pow(radius, 2) * (4f / 3f * radius + height)) / totalVolume;
            }
            else
            {
                var size = t.GetComponent<BoxCollider>().size;
                /*t.GetComponent<Rigidbody>().mass =
                    totalMass * size.x*size.y*size.z / totalVolume;*/
            }
        }

        radius = root.GetComponent<CapsuleCollider>().radius;
        height = root.GetComponent<CapsuleCollider>().height;
        root.GetComponent<Rigidbody>().mass =
            totalMass * (float) (Math.PI * Math.Pow(radius, 2) * (4f / 3f * radius + height)) / totalVolume;
    }
}

/*[System.Serializable]
public class Controller
{
    [HideInInspector] public string name;

    public GameObject bone; //reference

    [HideInInspector] public bool isRoot;

    public float targetPosition;
}*/

public class NamedArrayAttribute : PropertyAttribute
{
    public readonly string[] names;

    public NamedArrayAttribute(string[] names)
    {
        this.names = names;
    }
}