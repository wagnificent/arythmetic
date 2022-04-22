using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour {

	public Text feedbackDisplay;
	public Image feedbackBG;
	[Tooltip ("Duration of feedback messages (in seconds)")]public float feedbackDuration;

	public Text warningDisplay;
	public Image warningBG;
	[Tooltip("Duration of warning messages (in seconds)")]public float warningDuration;

	public Text firstElementDisplay;
	public Text operandDisplay;
	public Text secondElementDisplay;

	public Text scoreDisplay;
	public Text multiplierDisplay;
	public Slider multiplierTimer;

	public Text bombCountDisplay;
	public Text swapCountDisplay;
	public Text mulliganCountDisplay;
	public Text purgeCountDisplay;

	// Use this for initialization
	void Start () {
		feedbackBG.gameObject.SetActive (false);
		feedbackDisplay.gameObject.SetActive (false);
		warningBG.gameObject.SetActive (false);
		warningDisplay.gameObject.SetActive (false);

		firstElementDisplay.text = "";
		operandDisplay.text = "";
		secondElementDisplay.text = "";
	}

	//Congratulate the player
	public void GiveFeedback(string feedback, Color textColor){
		feedbackBG.gameObject.SetActive (true);
		feedbackDisplay.gameObject.SetActive (true);
		feedbackDisplay.color = textColor;
		feedbackDisplay.text = feedback;
		Invoke ("ClearFeedback", feedbackDuration);
	}

	//Warn the player
	public void GiveWarning(string warning){
		warningBG.gameObject.SetActive (true);
		warningDisplay.gameObject.SetActive (true);
		warningDisplay.text = warning;
		Invoke ("ClearWarning", warningDuration);
	}

	//Clear the warning display
	void ClearWarning (){
		warningBG.gameObject.SetActive (false);
		warningDisplay.gameObject.SetActive (false);
	}

	//Clear the feedback display
	void ClearFeedback (){
		feedbackBG.gameObject.SetActive (false);
		feedbackDisplay.gameObject.SetActive (false);
	}

	//Show current expression in the UI
	public void UpdateExpression (List<string> source, Text targetDisplay){
		string element = "";
		foreach (string value in source){
			element += value;
		}
		targetDisplay.text = element;
	}

	//Change the text of a display
	public void UpdateDisplay (string source, Text targetDisplay){
		targetDisplay.text = source;
	}
}
