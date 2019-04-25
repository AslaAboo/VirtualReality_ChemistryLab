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
        if(col.gameObject.tag=="CopperSoluble")
        {
            //System.Threading.Thread.Sleep(2000);
            rend.sharedMaterial = material[2];
        }

		if(col.gameObject.tag=="LeadInsoluble")
        {
            //System.Threading.Thread.Sleep(2000);
            //rend.sharedMaterial = material[2];
        }
		
		if(col.gameObject.tag=="AmmeniaSoluble")
        {
            //System.Threading.Thread.Sleep(2000);
            //rend.sharedMaterial = material[2];
        }
		
		    }

}

