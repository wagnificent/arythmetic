 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator : MonoBehaviour {

	public float Add (float firstElement, float secondElement){
		return (firstElement + secondElement);
	}

	public float Substract (float firstElement, float secondElement) {
		return (firstElement - secondElement);
	}

	public float Multiply (float firstElement, float secondElement) {
		return (firstElement * secondElement);
	}

	public float Divide (float firstElement, float secondElement) {
		return Mathf.Round((firstElement / secondElement)*100)/100;
	}
}
