using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour {

	[Tooltip("Maximum number of blocks that can be selected")] public int selectionLimit;
	private int numberOfBlocksSelected;
	private bool firstBlockIsSelected;

	private float lastBlockX;
	private float lastBlockY;

	private Block block;
	private DisplayManager displayManager;

	void Start () {
		displayManager = FindObjectOfType<DisplayManager> ();

		numberOfBlocksSelected = 0;
		firstBlockIsSelected = false;
	}

	//Determine whether the clicked block is a valid selection
	public bool CanSelect (float thisBlockX, float thisBlockY){

		float distanceX = Mathf.Abs(Mathf.Abs(thisBlockX) - Mathf.Abs(lastBlockX));
		float distanceY = Mathf.Abs(Mathf.Abs(thisBlockY) - Mathf.Abs(lastBlockY));

		//Do not select the block if the selection limit is reached
		if (numberOfBlocksSelected >= selectionLimit){
			displayManager.GiveWarning ("Tip: Up to " + selectionLimit + " blocks can be selected");
			return false;

		//Select the block if it's the first selection
		} else if (!firstBlockIsSelected){
			lastBlockX = thisBlockX;
			lastBlockY = thisBlockY;
			firstBlockIsSelected = true;
			numberOfBlocksSelected++;
			return true;
		
		//Select the block if it's adjacent to the previous selection
		} else if (
			(distanceX <= 0.1f && distanceY <= 1.1f) ||
			(distanceX <= 1.1f && distanceY <= 0.1f)){
				lastBlockX = thisBlockX;
				lastBlockY = thisBlockY;
				numberOfBlocksSelected++;
				return true;
		
		//Do not select the block
		} else {
			displayManager.GiveWarning ("Tip: Blocks must be adjacent");
			return false;
		}

	}

	//Reset all values, and deselect all blocks
	public void ClearSelections(){

		//Reset all values
		numberOfBlocksSelected = 0;
		firstBlockIsSelected = false;

		//Deselect all blocks
		Block[] blocksInPlay = FindObjectsOfType (typeof(Block)) as Block[];
		foreach (Block blockInPlay in blocksInPlay){
			if (blockInPlay.isSelected == true){
				blockInPlay.Deselect ();
			}
		}
	}
}
