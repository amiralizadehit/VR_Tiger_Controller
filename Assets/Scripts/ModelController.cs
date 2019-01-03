using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Networking;

public class ModelController : MonoBehaviour
{



    

    public List<Controller> controllers;
    private GameObject[] bones;


    private Rigidbody body;
    private HingeJoint hinge;
    private bool isInitialized;

    void Awake()
    {
        bones = GameObject.FindGameObjectsWithTag("Bone");
        if(controllers.Count==0)
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
                    controllers.Add(new Controller()
                    {
                        name = bone.name,
                        rigidbody = new RigidBodyController()
                        {
                            mass = body.mass
                        },
                        hinge = new HingJointController()
                        {
                            axis = hinge.axis,

                            useLimits = hinge.useLimits,
                            limitMin = hinge.limits.min,
                            limitMax = hinge.limits.max,

                            useSpring = hinge.useSpring,
                            damper = hinge.spring.damper,
                            spring = hinge.spring.spring,
                            targetPosition = hinge.spring.targetPosition
                        },
                        bone = bone
                    });
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
            controller.bone.GetComponent<Rigidbody>().mass = controller.rigidbody.mass;
            controller.bone.GetComponent<HingeJoint>().axis = controller.hinge.axis;
            JointLimits limit = new JointLimits()
            {
                max = controller.hinge.limitMax,
                min = controller.hinge.limitMin,
                bounceMinVelocity = 0.2f,
                bounciness = 0
            };
            controller.bone.GetComponent<HingeJoint>().limits = limit;
            controller.bone.GetComponent<HingeJoint>().useLimits = controller.hinge.useLimits;


            JointSpring spring = new JointSpring()
            {
                spring = controller.hinge.spring,
                damper = controller.hinge.damper,
                targetPosition = controller.hinge.targetPosition
            };
            controller.bone.GetComponent<HingeJoint>().spring = spring;
            controller.bone.GetComponent<HingeJoint>().useSpring = controller.hinge.useSpring;
        }
    }
}

[System.Serializable]
public class Controller
{
    public string name;
    public RigidBodyController rigidbody;
    public HingJointController hinge;

    public GameObject bone; //reference

}

[System.Serializable]
public class RigidBodyController
{
    public float mass;
}

[System.Serializable]
public class HingJointController
{
    public Vector3 axis;

    public bool useLimits;
    public float limitMin;
    public float limitMax;

    public bool useSpring;
    public float damper;
    public float spring;
    public float targetPosition;
}


