using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerPanel : MonoBehaviour {
	public InputField inputField;

	private float answer;
	private ExpressionManager expressionManager;


	void Start () {
		expressionManager = FindObjectOfType<ExpressionManager> ();
	}

	//Create shortcut keys for Submit and Cancel
	void Update() {
		if (Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetKeyDown (KeyCode.Return)) {
			submitAnswer ();
		}

		if (Input.GetKeyDown(KeyCode.Escape)){
			cancelAttempt ();
		}
	}

	//Store the current input as the answer
	public void setAnswer(){
		answer = float.Parse (inputField.text);
	}

	//Open the answer panel
	public void attemptAnswer(){
		gameObject.SetActive (true);
		inputField.Select ();

	}

	//Close the answer panel
	public void cancelAttempt(){
		gameObject.SetActive (false);
		inputField.text = "";
	}

	//Submit the current answer
	public void submitAnswer(){
		expressionManager.CheckAnswer (answer);
		gameObject.SetActive (false);
		inputField.text = "";
	}
		
}
