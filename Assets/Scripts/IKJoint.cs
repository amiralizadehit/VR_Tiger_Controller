using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKJoint : MonoBehaviour
{

    public HingeJoint hing;
    public float offset = 0;

    [SerializeField] public Mesh Cylinder;

    private Transform goal;
    private Transform endEffector;


	void Start ()
    {
        
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (goal!=null && endEffector!=null)
        {
            var position = transform.TransformPoint(hing.anchor);
            var toGoal = (goal.position - position).normalized;
            var toEF = (endEffector.position - position).normalized;
            var theta = Mathf.Acos(Vector3.Dot(toGoal, toEF)) * Mathf.Rad2Deg;

            var crossSign = Mathf.Sign(Vector3.Cross(toGoal,toEF).x);
            print(theta);

            var jointSpring = new JointSpring()
            {
                spring = hing.spring.spring,
                damper = hing.spring.damper,
                targetPosition = -crossSign * theta
            };
            hing.spring = jointSpring;
            //print(theta);
        }

    }

    public void SetGoal(Transform goal)
    {
        this.goal = goal;
    }

    public void SetEffector(Transform endEffector)
    {
        this.endEffector = endEffector;
    }

    private void OnDrawGizmos()
    {
        var position = transform.TransformPoint(hing.anchor);
        Gizmos.color = new Color(Color.white.r, Color.white.g, Color.white.b, 0.5f);
        Gizmos.DrawSphere(position,0.03f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(position,hing.connectedBody.transform.position);
    }
}
