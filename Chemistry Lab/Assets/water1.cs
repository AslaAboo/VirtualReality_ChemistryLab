using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water1 : MonoBehaviour {

    public Material[] material;
    Renderer rend;
	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
	}

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag=="salt")
        {
            rend.sharedMaterial = material[1];
        }

		
		
		
		
		//else
        //{
        //    rend.sharedMaterial = material[2];
        //}
    }

}