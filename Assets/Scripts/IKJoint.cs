using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScript.Macros;

public class IKJoint : MonoBehaviour
{
    public HingeJoint hing;


    [SerializeField] public Mesh Cylinder;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void RunIK(Transform goal, Transform endEffector)
    {
      
        var max = hing.limits.max;
        var min = hing.limits.min;
        float currentTargetPosition = hing.spring.targetPosition;


        var position = transform.TransformPoint(hing.anchor);
        var toGoal = (goal.position - position).normalized;
        var toEF = (endEffector.position - position).normalized;
        var theta = Mathf.Acos(Vector3.Dot(toGoal, toEF)) * Mathf.Rad2Deg;

        var crossSign = Mathf.Sign(Vector3.Cross(toGoal, toEF).x);

        var jointSpring = new JointSpring()
        {
            spring = hing.spring.spring,
            damper = hing.spring.damper,
            targetPosition = Mathf.Clamp(-crossSign*0.1f + hing.spring.targetPosition, min, max)
        };

        hing.spring = jointSpring;
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