using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public bool shouldAutoLoad;

	void Start (){
		if (shouldAutoLoad){
			Invoke ("LoadNextLevel", 5f);
		}
	}

	public void LoadLevel (string name) {	
		SceneManager.LoadScene (name);
	}

	public void LoadNextLevel() {
		int nextSceneIndex = SceneManager.GetActiveScene ().buildIndex +1;
		SceneManager.LoadScene (nextSceneIndex);
	}
		
}
