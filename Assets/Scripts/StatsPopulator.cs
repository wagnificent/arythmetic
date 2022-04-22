using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsPopulator : MonoBehaviour {

	public Text score;
	public Text blocks;
	public Text expressions;
	public Text multiplier;
	public Text time;

	private StatsManager statsManager;

	// Use this for initialization
	void Start () {
		statsManager = FindObjectOfType<StatsManager> ();
		score.text = statsManager.topScore.ToString ();
		blocks.text = statsManager.blocksDestroyed.ToString ();
		expressions.text = statsManager.expressionsSolved.ToString ();
		multiplier.text = statsManager.topMultiplier.ToString ();
		time.text = statsManager.timePlayed.ToString () + " seconds";
	}
}
