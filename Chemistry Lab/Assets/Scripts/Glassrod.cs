using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glassrod : MonoBehaviour {

    public Material[] material;
    Renderer rend;
    //GameObject gameObject = new GameObject;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "CopperSoluble")
        {
            rend.sharedMaterial = material[1];
            rend.tag = "CopperSoluble";
            //col.gameObject.tag = "CopperSulphate";
        }
        if (col.gameObject.tag == "LeadInsoluble")
        {
            rend.sharedMaterial = material[2];
            rend.tag = "LeadInsoluble";
        }
        if (col.gameObject.tag == "AmmeniaSoluble")
        {
            rend.sharedMaterial = material[3];
            rend.tag = "AmmeniaSoluble";
        }

    }
}
