using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardsManager : MonoBehaviour {

	[Tooltip("Number of seconds the multiplier lasts")] public float multiplierLifetime;

	public bool isBombPrimed = false;
	public bool isSwapPrimed = false;
	public bool isFirstSwapSelected = false;

	public Block firstSwapBlock;
	public Block secondSwapBlock;

	private DisplayManager displayManager;
	private FXManager fxManager;
	private ExpressionManager expressionManager;
	private StatsManager statsManager;

	private int playerScore;
	private int currentMultiplier;
	private int powerUpIndex;
	private int bombCount;
	private int swapCount;
	private int mulliganCount;
	private int purgeCount;
	private float timeLeft;

	// Use this for initialization
	void Start () {
		
		displayManager = FindObjectOfType<DisplayManager> ();
		fxManager = FindObjectOfType<FXManager> ();
		expressionManager = FindObjectOfType<ExpressionManager> ();
		statsManager = FindObjectOfType<StatsManager> ();

		displayManager.multiplierTimer.maxValue = multiplierLifetime;
	}
	
	// Update is called once per frame
	void Update () {
		MultiplierCountdown ();
	}

	public void RewardPlayer(){

		int numberDestroyed = 0;

		Block[] blocksInPlay = FindObjectsOfType (typeof(Block)) as Block[];

		foreach (Block blockInPlay in blocksInPlay) {
			if (blockInPlay.isSelected == true) {
				blockInPlay.Deselect ();
				Destroy (blockInPlay.gameObject);
				numberDestroyed++;
			}
		}

		AwardPoints (numberDestroyed);
		CheckPowerUps ();

		statsManager.blocksDestroyed += numberDestroyed;
	}

	public void PunishPlayer(){
		timeLeft = 0;
	}

	//Add to the player's score
	void AwardPoints(int baseScore){
		currentMultiplier++;
		timeLeft = multiplierLifetime;
		playerScore = playerScore + (baseScore * currentMultiplier);

		displayManager.UpdateDisplay ("x " + currentMultiplier.ToString (), displayManager.multiplierDisplay);
		displayManager.UpdateDisplay (playerScore.ToString (), displayManager.scoreDisplay);

		statsManager.topScore = playerScore;
		statsManager.expressionsSolved++;
		if (currentMultiplier > statsManager.topMultiplier){
			statsManager.topMultiplier = currentMultiplier;
		}
	}

	//Reset the multiplier after the time limit
	void MultiplierCountdown(){

		timeLeft = timeLeft - Time.deltaTime;
		displayManager.multiplierTimer.value = timeLeft;
		if (timeLeft <= 0f){
			currentMultiplier = 1;
			displayManager.UpdateDisplay ("x " + currentMultiplier.ToString (), displayManager.multiplierDisplay);
		}

	}

	//Award a powerup if the player meets criteria
	void CheckPowerUps(){
		if (currentMultiplier % 5 == 0){
			powerUpIndex++;
			if (powerUpIndex == 5){
				powerUpIndex = 1;
			}
			if (powerUpIndex == 1){
				AwardBomb ();
			}
			if (powerUpIndex == 2){
				AwardSwap ();
			}
			if (powerUpIndex == 3){
				AwardMulligan ();
			}
			if (powerUpIndex == 4){
				AwardPurge ();
			}
		}

	}

	void AwardBomb(){
		bombCount++;
		displayManager.UpdateDisplay(bombCount.ToString(), displayManager.bombCountDisplay);
		displayManager.GiveWarning ("Bomb awarded");
		fxManager.PlayFX (4);
	}

	void AwardSwap(){
		swapCount++;
		displayManager.UpdateDisplay(swapCount.ToString (), displayManager.swapCountDisplay);
		displayManager.GiveWarning ("Swap awarded");
		fxManager.PlayFX (4);
	}

	void AwardMulligan(){
		mulliganCount++;
		displayManager.UpdateDisplay(mulliganCount.ToString (), displayManager.mulliganCountDisplay);
		displayManager.GiveWarning ("Mulligan awarded");
		fxManager.PlayFX (4);
	}

	void AwardPurge(){
		purgeCount++;
		displayManager.UpdateDisplay(purgeCount.ToString (), displayManager.purgeCountDisplay);
		displayManager.GiveWarning ("Purge awarded");
		fxManager.PlayFX (4);
	}

	//Reset all stats
	void HardReset(){
		currentMultiplier = 1;
		powerUpIndex = 0;
		playerScore = 0;
		bombCount = 0;
		swapCount = 0;
		mulliganCount = 0;
		purgeCount = 0;

		displayManager.UpdateDisplay ("x " + currentMultiplier.ToString (), displayManager.multiplierDisplay);
		displayManager.UpdateDisplay (playerScore.ToString (), displayManager.scoreDisplay);
		displayManager.UpdateDisplay(bombCount.ToString(), displayManager.bombCountDisplay);
		displayManager.UpdateDisplay(swapCount.ToString (), displayManager.swapCountDisplay);
		displayManager.UpdateDisplay(mulliganCount.ToString (), displayManager.mulliganCountDisplay);
		displayManager.UpdateDisplay(purgeCount.ToString (), displayManager.purgeCountDisplay);
	}

	public void PrimeBomb(){
		if (isSwapPrimed == true) {
			displayManager.GiveWarning ("Only one powerup can be used at a time");
		}
		else if (isBombPrimed == true){
			isBombPrimed = false;
			displayManager.GiveFeedback ("Bomb cancelled", Color.white);
		}
		else if (bombCount > 0 && !isBombPrimed) {
			displayManager.GiveFeedback ("Select a target to destroy", Color.white);
			isBombPrimed = true;
			expressionManager.Clear ();
		} else {
			displayManager.GiveWarning ("No bombs available");
		}
	}

	public void BombUsed(){
		isBombPrimed = false;
		bombCount--;
		displayManager.GiveFeedback ("Target destroyed", Color.white);
		displayManager.UpdateDisplay (bombCount.ToString (), displayManager.bombCountDisplay);
	}

	public void PrimeSwap(){
		if (isBombPrimed == true) {
			displayManager.GiveWarning ("Only one powerup can be used at a time");
		} 
		else if (isSwapPrimed == true) {
			isSwapPrimed = false;
			displayManager.GiveFeedback ("Swap cancelled", Color.white);
		}
		else if (swapCount > 0) {
			isSwapPrimed = true;
			displayManager.GiveWarning ("Select two targets to swap");
			expressionManager.Clear ();
		} else {
			displayManager.GiveWarning ("No swaps available");
		}
	}

	public void SwapUsed(){
		isSwapPrimed = false;
		swapCount--;
		displayManager.GiveFeedback ("Targets swapped", Color.white);
		displayManager.UpdateDisplay (swapCount.ToString (), displayManager.swapCountDisplay);
	}

	public void UseMulligan(){
		if (isBombPrimed == true || isSwapPrimed == true) {
			displayManager.GiveWarning ("Only one powerup can be used at a time");
		}
		else if (mulliganCount > 0) {
			Block[] allBlocks = FindObjectsOfType(typeof(Block)) as Block[];
			foreach(Block block in allBlocks){
				Destroy (block.gameObject);
			}
			expressionManager.Clear ();
			mulliganCount--;
			displayManager.GiveFeedback ("All blocks destroyed", Color.white);
			displayManager.UpdateDisplay (mulliganCount.ToString (), displayManager.mulliganCountDisplay);
		} else {
			displayManager.GiveWarning ("No mulligans available");
		}
	}

	public void UsePurge(){
		if (isBombPrimed == true || isSwapPrimed == true){
			displayManager.GiveWarning ("Only one powerup can be used at a time");
		}
		else if (purgeCount > 0) {
			Block[] allBlocks = FindObjectsOfType(typeof(Block)) as Block[];
			foreach(Block block in allBlocks){
				if (block.blockValue == "obstacle"){
				Destroy (block.gameObject);
				}
			}
			expressionManager.Clear ();
			purgeCount--;
			displayManager.GiveFeedback ("All obstacles destroyed", Color.white);
			displayManager.UpdateDisplay (purgeCount.ToString (), displayManager.purgeCountDisplay);
		} else {
			displayManager.GiveWarning ("No purges available");
		}
	}
}
