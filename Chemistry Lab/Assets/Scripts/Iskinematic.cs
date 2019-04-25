using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iskinematic : Interactive{
	
	// Update is called once per frame
	protected override void Start() {
        rigidbody.isKinematic = true;
	}
}
