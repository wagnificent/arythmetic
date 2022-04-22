using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpressionManager : MonoBehaviour {

	public AnswerPanel answerPanel;

	private Calculator calculator;
	private RewardsManager rewardsManager;
	private SelectionManager selectionManager;
	private DisplayManager displayManager;
	private FXManager fxManager;


	private List<string> memoryA;
	private List<string> memoryB;
	private List<string> memoryO;
	private string[] equationElements;

	private bool firstElementIsEntered;
	private bool expressionIsReady;

	private float calculatorAnswer;




	void Start () {
		calculator = FindObjectOfType<Calculator> ();
		rewardsManager = FindObjectOfType<RewardsManager> ();
		selectionManager = FindObjectOfType<SelectionManager> ();
		displayManager = FindObjectOfType<DisplayManager> ();
		fxManager = FindObjectOfType<FXManager> ();

		memoryA = new List<string> ();
		memoryB = new List<string> ();
		memoryO = new List<string> ();
		equationElements = new string[3];

	}

	//Create shortcut keys for Solve and Clear
	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Clear ();
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			TryAnswer ();
		}
	}


	//Check if the selected input is valid 
	public bool CanAdd(string checkInput, float xPos, float yPos){

		int operandTest;

		//Log a warning if the input is an obstacle
		if (checkInput == "obstacle"){
			displayManager.GiveWarning ("Tip: Obstacles cannot be selected");
			return false;
		}

		//Log a warning if the first input is an operand
		else if (displayManager.firstElementDisplay.text == "" && !int.TryParse (checkInput, out operandTest)) {
			displayManager.GiveWarning ("Tip: Expression must begin with an integer");
			return false;
		} 

		//Log a warning if more than one operand is input
		else if (firstElementIsEntered == true && !int.TryParse (checkInput, out operandTest)) {
			displayManager.GiveWarning ("Tip: Only one operand per expression is accepted");
			return false;
		}

		//Verify that the selection is valid
		else if (!selectionManager.CanSelect (xPos, yPos)){
			return false;
		}

		else {
			AddToEquation (checkInput);
			return true;
		}
	}

	//Build valid inputs into the expression
	void AddToEquation(string newValue){

		int operandDetector;

		//Store the operand, and start building the second element
		if (!int.TryParse (newValue, out operandDetector)) {
			memoryO.Add (newValue);
			displayManager.UpdateExpression (memoryO, displayManager.operandDisplay);
			firstElementIsEntered = true;
		} 

		//Build the first element
		else if (!firstElementIsEntered){
			memoryA.Add (newValue);
			displayManager.UpdateExpression (memoryA, displayManager.firstElementDisplay);
		}

		//Build the second element
		else if (firstElementIsEntered){
			memoryB.Add (newValue);
			expressionIsReady = true;
			displayManager.UpdateExpression (memoryB, displayManager.secondElementDisplay);
		} 

	}

	//Reset the values and selections
	public void Clear(){

		memoryA = new List<string> ();
		memoryB = new List<string> ();
		memoryO = new List<string> ();
		equationElements = new string[3];
		firstElementIsEntered = false;
		expressionIsReady = false;

		displayManager.UpdateDisplay ("", displayManager.firstElementDisplay);
		displayManager.UpdateDisplay ("", displayManager.operandDisplay);
		displayManager.UpdateDisplay ("", displayManager.secondElementDisplay);

		selectionManager.ClearSelections ();

		fxManager.PlayFX (1);

	}

	//Calculate the answer to the current expression
	void GetCalculatorAnswer(){

		//Update the first element
		equationElements [0] = "";
		foreach (string memory in memoryA) {
			equationElements [0] += memory;
		}

		equationElements [1] = "";
		foreach (string memory in memoryO) {
			equationElements [1] += memory;
		}

		//Update the second element
		equationElements [2] = "";
		foreach (string memory in memoryB) {
			equationElements [2] += memory;
		}

		if (equationElements [1] == "+") {
			calculatorAnswer = calculator.Add (float.Parse(equationElements[0]), float.Parse(equationElements[2]));
		} else if (equationElements [1] == "-") {
			calculatorAnswer = calculator.Substract (float.Parse(equationElements[0]), float.Parse(equationElements[2]));
		} else if (equationElements [1] == "*") {
			calculatorAnswer = calculator.Multiply (float.Parse(equationElements[0]), float.Parse(equationElements[2]));
		} else if (equationElements [1] == "/") {
			calculatorAnswer = calculator.Divide (float.Parse(equationElements[0]), float.Parse(equationElements[2]));
		}

	}

	//Check the player's answer
	public void CheckAnswer(float playerAnswer){

		GetCalculatorAnswer ();

		if (calculatorAnswer == playerAnswer) {
			CorrectAnswer();
		} else {
			IncorrectAnswer ();
		}
	}

	public void TryAnswer(){
		if (expressionIsReady) {
			answerPanel.attemptAnswer ();
		} else {
			displayManager.GiveWarning ("Expression is incomplete");
		}
	}


	void CorrectAnswer() {
		rewardsManager.RewardPlayer ();
		displayManager.GiveFeedback ("Correct!", Color.green);
		Clear ();
		fxManager.PlayFX (2);
	}

	void IncorrectAnswer(){
		rewardsManager.PunishPlayer ();
		displayManager.GiveFeedback ("Incorrect", Color.yellow);
		Clear ();
		fxManager.PlayFX (3);
	}

}
