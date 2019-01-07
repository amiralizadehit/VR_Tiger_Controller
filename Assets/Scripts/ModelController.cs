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
        "Waist",
        "Neck",
        "Right Leg Toe",
        "Right Leg Shin",
        "Right Leg Thigh",
        "Right Hand Toe",
        "Right Hand Forearm",
        "Right Hand Arm",
        "Left Leg Toe",
        "Left Leg Shin",
        "Left Leg Thigh",
        "Left Hand Toe",
        "left Hand Forearm",
        "Left Hand Arm",
        "Left Leg Upper",
        "Right Leg Upper"
    })]
    public float[] parts;


    public GameObject root;
    public GameObject[] bones;


    void Awake()
    {
        Time.fixedDeltaTime = 0.01f;

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
        for (int i = 0; i < bones.Length; i++)
        {
            JointSpring spring = new JointSpring()
            {
                spring = (i == 0) ? modelSpring / 2 : modelSpring,
                damper = modelDamper,
                targetPosition = parts[i]
            };
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