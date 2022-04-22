using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour {

	const string MUSIC_VOLUME_KEY = "music_volume";
	const string DISABLE_MUSIC_KEY = "disable_music";
	const string FX_VOLUME_KEY = "fx_volume";
	const string DISABLE_FX_KEY = "disable_fx";

	public static void SetMusicVolume(float newVolume){
		if (newVolume >= 0f && newVolume <= 1f){
			PlayerPrefs.SetFloat ("music_volume", newVolume);	
		}
	}

	public static float GetMusicVolume(){
		return PlayerPrefs.GetFloat ("music_volume");
	}

	public static void SetMusicToggle(float newValue){
		if (newValue == 1){
			PlayerPrefs.SetFloat ("disable_music", 1);
		} else if (newValue == 0){
			PlayerPrefs.SetFloat ("disable_music", 0);
		}
	}

	public static float GetMusicToggle(){
		return PlayerPrefs.GetFloat ("disable_music");
	}

	public static void SetFXVolume(float newVolume){
		if (newVolume >= 0f && newVolume <= 1f){
			PlayerPrefs.SetFloat ("fx_volume", newVolume);
		}
	}

	public static float GetFXVolume(){
		return PlayerPrefs.GetFloat ("fx_volume");
	}
		
	public static void SetFXToggle(float newValue){
		if (newValue == 1){
			PlayerPrefs.SetFloat ("disable_fx", 1);
		} else if (newValue == 0){
			PlayerPrefs.SetFloat ("disable_fx", 0);
		}
	}

	public static float GetFXToggle(){
		return PlayerPrefs.GetFloat ("disable_fx");
	}
}
