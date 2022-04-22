using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

	public Slider musicSlider;
	public Toggle musicToggle;
	public AudioClip[] musicTracks;

	private bool isMusicDisabled;

	private AudioSource audioSource;

	void Start () {
		audioSource = GetComponent<AudioSource> ();

		musicSlider.value = PlayerPrefsManager.GetMusicVolume ();

		if (PlayerPrefsManager.GetMusicToggle () == 1){
			isMusicDisabled = true;
			musicToggle.isOn = true;
		} else {
			isMusicDisabled = false;
			musicToggle.isOn = false;
		}

		if (!isMusicDisabled){
			StartMusic ();	
		}
	}

	void Update(){
		if (!isMusicDisabled){
			AutoLoadNextTrack ();	
		}
	}

	void StartMusic(){
		int randomTrack = Random.Range (0, musicTracks.Length);
		audioSource.clip = musicTracks [randomTrack];
		audioSource.Play ();
	}

	void AutoLoadNextTrack(){
		if (!isMusicDisabled && !audioSource.isPlaying){
			int nextTrack = Random.Range (1, musicTracks.Length);
			audioSource.clip = musicTracks [nextTrack];
			audioSource.Play ();
		}
	}

	public void AdjustMusicVolume (){
		audioSource.volume = musicSlider.value;
		PlayerPrefsManager.SetMusicVolume (musicSlider.value);
	}

	public void ToggleMusic(){
		isMusicDisabled = !isMusicDisabled;
		if (isMusicDisabled == true) {
			audioSource.Pause ();
			PlayerPrefsManager.SetMusicToggle (1);
		} else if (isMusicDisabled == false) {
			audioSource.Play ();
			PlayerPrefsManager.SetMusicToggle (0);
		}
	}
		

}
