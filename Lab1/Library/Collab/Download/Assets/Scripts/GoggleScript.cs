using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoggleScript : MonoBehaviour {

    public GameObject goggles;
    public bool isOn;


    void FixedUpdate()
    {


        Debug.Log("isGoggle");
        if (isOn == true)
        {
            goggles.transform.parent = this.transform;
            goggles.transform.localScale = new Vector3(0, 0, 0);
            goggles.transform.localPosition = new Vector3(1.73f, 0.711f, -0.829f);
            goggles.transform.localEulerAngles = new Vector3(90, 150, 0);
            //goggles.GetComponent<Renderer>().enabled = false;
            //goggles.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            //goggles.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);

            goggles.GetComponent<Rigidbody>().useGravity = true;
        }
        else if (isOn == false)
        {
            goggles.transform.parent = this.transform;
            goggles.transform.localScale = new Vector3(7.399937f, 6.635272f, 4.626822f);
            goggles.transform.localPosition = new Vector3(1.73f, 0.711f, -0.829f);
            goggles.transform.localEulerAngles = new Vector3(90, 150, 0);
            //goggles.GetComponent<Renderer>().enabled = true;
            //goggles.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            //goggles.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);

            goggles.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}

