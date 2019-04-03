using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoggleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Goggles>())
        {

            if (other.GetComponent<FixedJoint>())
                other.GetComponent<FixedJoint>().connectedBody.GetComponent<VirtualHand>();// Detach code
            other.transform.parent = this.transform;
            other.transform.localPosition = new Vector3(0.0441f, -0.19639f, 0.0930f);
            other.transform.localEulerAngles = new Vector3(0, 0, 0);
            other.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            other.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);

            other.GetComponent<Rigidbody>().useGravity = false;

        }

    }
}

