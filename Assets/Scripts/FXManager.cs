using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FXManager : MonoBehaviour {

	public Slider fxSlider;
	public Toggle fxToggle;
	public AudioClip[] fxClips;

	private bool isFXDisabled;

	public AudioSource audioSourceA;
	public AudioSource audioSourceB;

	// Use this for initialization
	void Start () {
		if (PlayerPrefsManager.GetFXToggle() == 1){
			isFXDisabled = true;
			fxToggle.isOn = true;
		} else {
			isFXDisabled = false;
			fxToggle.isOn = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AdjustFXVolume(){
		audioSourceA.volume = fxSlider.value;
		audioSourceB.volume = fxSlider.value;
		PlayerPrefsManager.SetFXVolume (fxSlider.value);
	}

	public void PlayFX (int clipNumber){
		if (!isFXDisabled && clipNumber < 4) {
			audioSourceA.clip = fxClips [clipNumber];
			audioSourceA.Play ();	
		} else if (!isFXDisabled && clipNumber >= 4) {
			audioSourceB.clip = fxClips [clipNumber];
			audioSourceB.Play ();
		}

	}

	public void ToggleFX (){
		isFXDisabled = !isFXDisabled;
		if (isFXDisabled == true){
			PlayerPrefsManager.SetFXToggle (1);
		} else if (isFXDisabled == false){
			PlayerPrefsManager.SetFXToggle (0);
		}
	}
}
