using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject blockPrefab;
	public int chanceOfSymbol;
	public int chanceOfObstacle;
	public Sprite[] integers;
	public Sprite[] symbols;
	public Sprite[] obstacles;

	private float spawnDelay = 0.5f;
	private float lastSpawnTime = 0f;
	private int index;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.childCount < 8 && Time.time - lastSpawnTime >= spawnDelay) {
			SpawnBlock ();
		}
	}

	void SpawnBlock(){
		GameObject newBlock = Instantiate (blockPrefab, transform.position, Quaternion.identity, this.transform) as GameObject;
		Block block = newBlock.GetComponent<Block> ();
		SpriteRenderer blockSprite = newBlock.GetComponent<SpriteRenderer> ();

		//randomly select whether the new block will be a symbol, an obstacle, or an integer
		int characterType = Random.Range (1, 100);

		if (characterType <= chanceOfSymbol) {
			index = Random.Range (0, 4);
			blockSprite.sprite = symbols [index];
			block.SymbolToString (index);
		} 

		else if (characterType >= 101 - chanceOfObstacle) {
			index = Random.Range (0, 1);
			blockSprite.sprite = obstacles [index];
			block.blockValue = "obstacle";
		} 

		else {
			index = Random.Range (0, 9);
			blockSprite.sprite = integers [index];
			block.blockValue = index.ToString();
		}

		//reset the spawn timer
		lastSpawnTime = Time.time;
	}
}
