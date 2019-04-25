using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatulaFill : MonoBehaviour {
	public GameObject objToDestroy;
	public GameObject objToSpawn;
	// Use this for initialization
	void Start () {
		Destroy(objToDestroy);
		Instantiate(objToSpawn,objToDestroy.transform.position,objToDestroy.transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
			}
	void onTriggerEnter(Collider other)
	{
	}
				
}
