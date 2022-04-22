using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsManager : MonoBehaviour {

	public int topScore = 0;
	public int topMultiplier = 0;
	public int blocksDestroyed = 0;
	public int expressionsSolved = 0;
	public float timePlayed = 0;



	void Awake(){
		if (SceneManager.GetActiveScene().buildIndex < 3){
			DontDestroyOnLoad (this.gameObject);	
		}

	}

	public void CaptureTimePlayed(){
		timePlayed = Mathf.Round (Time.timeSinceLevelLoad);
	}
		
}
