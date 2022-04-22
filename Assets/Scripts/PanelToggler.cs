using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelToggler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ToggleOn(){
		this.gameObject.SetActive (true);
	}

	public void ToggleOff(){
		this.gameObject.SetActive (false);
	}
}
