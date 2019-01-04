using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Networking;

public class ModelController : MonoBehaviour
{
    public float totalVolume;
    public float totalMass;
    public float modelDamper;
    public float modelSpring;
    public List<Controller> controllers;


    private GameObject[] bones;
    private Rigidbody body;
    private HingeJoint hinge;
    private CapsuleCollider collider;
    private bool isInitialized;


    void Awake()
    {
        bones = GameObject.FindGameObjectsWithTag("Bone");
        if (controllers.Count == 0)
            controllers = new List<Controller>(bones.Length);
        else
        {
            isInitialized = true;
            UpdateModel();
        }
    }

    void Start()
    {
        if (!isInitialized)
        {
            foreach (var bone in bones)
            {
                if (bone != null)
                {
                    body = bone.GetComponent<Rigidbody>();
                    hinge = bone.GetComponent<HingeJoint>();
                    collider = bone.GetComponent<CapsuleCollider>();

                    controllers.Add(new Controller()
                    {
                        name = bone.name,
                        targetPosition = (hinge!=null) ? hinge.spring.targetPosition : 0,
                        isRoot = (hinge == null),
                        bone = bone
                    });

                    totalVolume += (float) (Math.PI * Math.Pow(collider.radius, 2) *
                                            (4f / 3f * collider.radius + collider.height));
                }
            }
        }
    }

    void Update()
    {
        UpdateModel();
    }


    private void UpdateModel()
    {
        foreach (var controller in controllers)
        {
            var radius = controller.bone.GetComponent<CapsuleCollider>().radius;
            var height = controller.bone.GetComponent<CapsuleCollider>().height;
            if (!controller.isRoot) // we don't have hing in root bone
            {
                JointSpring spring = new JointSpring()
                {
                    spring = modelSpring,
                    damper = modelDamper,
                    targetPosition = controller.targetPosition,
                };
                controller.bone.GetComponent<HingeJoint>().spring = spring;
            }
            controller.bone.GetComponent<Rigidbody>().mass =
                totalMass * (float) (Math.PI * Math.Pow(radius, 2) * (4f / 3f * radius + height)) / totalVolume;
        }
    }
}

[System.Serializable]
public class Controller
{
    [HideInInspector] public string name;

    public GameObject bone; //reference

    [HideInInspector] public bool isRoot;

    public float targetPosition;
}