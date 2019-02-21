using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerRoot : MonoBehaviour
{

    private Vector3 initPosition;
	// Use this for initialization
	void Start ()
    {
        initPosition = transform.position;
    }
	
	// Update is called once per frame
    public Vector3 GetRootTransform()
    {
        return transform.position; // - initPosition;
    }
}
