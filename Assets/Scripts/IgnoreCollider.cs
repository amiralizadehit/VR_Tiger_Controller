using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollider : MonoBehaviour
{
	void Start () {

        Physics.IgnoreCollision(gameObject.GetComponent<BoxCollider>(),transform.parent.GetComponent<CapsuleCollider>());
	}

}
