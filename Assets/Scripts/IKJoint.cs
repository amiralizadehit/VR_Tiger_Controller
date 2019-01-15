using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKJoint : MonoBehaviour
{

    public HingeJoint hing;

    [SerializeField] public Mesh Cylinder;


	void Start ()
    {
        
        
    }
	
	// Update is called once per frame
	void Update () {
        var d = transform.TransformPoint(hing.anchor);
        //(hing.angle);
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
