using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClock : MonoBehaviour {

	private Text gameClock;

	private float seconds;
	private float minutes;

	// Use this for initialization
	void Start () {
		gameClock = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		minutes = Mathf.Round(Time.timeSinceLevelLoad / 60f);
		seconds = Mathf.Round (Time.timeSinceLevelLoad % 60f);
		if (minutes >= 10f && seconds < 10f){
			gameClock.text = minutes + ":" + "0" + seconds;
		}else if (minutes < 10f && seconds >= 10f){
			gameClock.text = "0" + minutes + ":" + seconds;
		}else if (minutes < 10f && seconds < 10f) {
			gameClock.text = "0" + minutes + ":" + "0" + seconds;
		} else {
			gameClock.text = minutes + ":" + seconds;
		}
	}
}
