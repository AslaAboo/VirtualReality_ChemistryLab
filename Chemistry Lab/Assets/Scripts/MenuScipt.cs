using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScipt : MonoBehaviour {

	public void practiceLab (string SceneName) {
		Debug.Log("Practice");
		Application.LoadLevel(SceneName);
	}
	public void testLab (string SceneName) {
		Debug.Log("Practice");
		Application.LoadLevel(SceneName);
	}
	public void Quit(){
		Debug.Log("quit");
		Application.Quit();
	}
	//public void FixedUpdate(){
		//SceneManager.LoadScene("LabLearn");
	//}
	
}
