using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGate : MonoBehaviour
{

	public GameController gameController;

	// Use this for initialization
	void Start ()
	{
		gameController = FindObjectOfType<GameController>();	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<PlayerController>() != null && gameController.playerWon)
		{
			gameController.gameState = GameController.GameStates.LevelComplete;
			Debug.Log("LOADING NEXT LEVEL");
		}
	}
}
