using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeAudio : MonoBehaviour {

	public GameObject Labels;
	public float deploymentHeight;
	private bool deployed;
	// Use this for initialization
	void Start () {
		AudioSource audio = GetComponent<AudioSource>();
		//audio.Play();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		RaycastHit hit;
		Ray landingRay = new Ray(transform.position, Vector3.down);
		//PlayWelcomeAudio();
		if(!deployed)
		{
			if(Physics.Raycast(landingRay, out hit, deploymentHeight))
			{
					if(hit.collider.tag=="Labels")
					{
							PlayWelcomeAudio();
					}
			}
		}
	}
	void PlayWelcomeAudio(){
		AudioSource audio = GetComponent<AudioSource>();
		audio.Play();
	}
	
}
