using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScript.Macros;

public class IKJoint : MonoBehaviour
{
    public HingeJoint hing;

    

    public Transform goal;
    public Transform endEffector;

    public bool isLeg = false;

    private float max;
    private float min;
    private Vector3 initPosition;

    [SerializeField] public Mesh Cylinder;


    void Start()
    {
        max = hing.limits.max;
        min = hing.limits.min;

        

    }

    // Update is called once per frame
    void Update()
    { 
        RunIK();
    }

    


    public void RunIK()
    {
        var theta = CalculateTheta();
        if (theta>10)
        {
            var crossSign = GetSign();

            var jointSpring = new JointSpring()
            {
                spring = hing.spring.spring,
                damper = hing.spring.damper,
                targetPosition = Mathf.Clamp(crossSign*1.2f + hing.spring.targetPosition, min, max)
            };
            hing.spring = jointSpring;
            theta = CalculateTheta();
        }
        
    }

    private float CalculateTheta()
    {
       
        var toGoal = GetNormalizedDirection(goal.position);
        var toEF = GetNormalizedDirection(endEffector.position);
        var theta = Mathf.Acos(Vector3.Dot(toGoal, toEF)) * Mathf.Rad2Deg;

        return theta;
    }

    private Vector3 GetNormalizedDirection(Vector3 goal)
    {
        var position = transform.TransformPoint(hing.anchor);
        return (goal - position).normalized;
    }

    private float GetSign()
    {
  
        var toGoal = GetNormalizedDirection(goal.position);
        var toEF = GetNormalizedDirection(endEffector.position);
        var sign = Mathf.Sign(Vector3.Cross(toGoal, toEF).x);
        return (isLeg)?sign:-sign;
    }


    private void OnDrawGizmos()
    {
        var position = transform.TransformPoint(hing.anchor);
        Gizmos.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.5f);
        Gizmos.DrawSphere(position, 0.03f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(position, hing.connectedBody.transform.position);
    }
}