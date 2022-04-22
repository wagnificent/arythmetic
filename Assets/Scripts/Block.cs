using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour {

	public string blockValue;
	public bool isSelected = false;

	private SpriteRenderer spriteRenderer;
	private ExpressionManager expressionManager;
	private FXManager fxManager;
	private RewardsManager rewardsManager;


	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		expressionManager = FindObjectOfType<ExpressionManager> ();
		fxManager = FindObjectOfType<FXManager> ();
		rewardsManager = FindObjectOfType<RewardsManager> ();
	}


	//Set this block's value to the appropriate mathematical symbol
	public void SymbolToString (int index){
		if (index == 0) {
			blockValue = "+";
		} else if (index == 1) {
			blockValue = "-";
		} else if (index == 2) {
			blockValue = "*";
		} else if (index == 3) {
			blockValue = "/";
		}
	}

	//Click to select the block
	void OnMouseUp (){
		if (!EventSystem.current.IsPointerOverGameObject () && rewardsManager.isBombPrimed == true) {
				Destroy (this.gameObject);
				rewardsManager.BombUsed ();
		} 

		else if (!EventSystem.current.IsPointerOverGameObject () && rewardsManager.isSwapPrimed == true && rewardsManager.isFirstSwapSelected == false) {
			rewardsManager.firstSwapBlock = this;
			rewardsManager.firstSwapBlock.spriteRenderer.color = Color.blue;
			rewardsManager.isFirstSwapSelected = true;
		} 

		else if (!EventSystem.current.IsPointerOverGameObject () && rewardsManager.isSwapPrimed == true && rewardsManager.isFirstSwapSelected == true) {

			rewardsManager.firstSwapBlock.spriteRenderer.color = Color.white;

			Sprite storedSprite = rewardsManager.firstSwapBlock.spriteRenderer.sprite;
			string storedValue = rewardsManager.firstSwapBlock.blockValue;

			rewardsManager.firstSwapBlock.blockValue = this.blockValue;
			rewardsManager.firstSwapBlock.spriteRenderer.sprite = this.spriteRenderer.sprite;
			this.blockValue = storedValue;
			this.spriteRenderer.sprite = storedSprite;

			rewardsManager.SwapUsed ();
		}
		else if (!EventSystem.current.IsPointerOverGameObject() && !isSelected && expressionManager.CanAdd(blockValue, transform.position.x, transform.position.y) == true){
				Select ();	
				fxManager.PlayFX (0);
		}
	}

	//Select the block if it is not currently selected
	public void Select (){
		isSelected = true;
		spriteRenderer.color = Color.yellow;
	}

	//Deselect the block
	public void Deselect(){
		isSelected = false;
		spriteRenderer.color = Color.white;
	}
}
