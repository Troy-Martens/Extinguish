using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public float playerMoney = 0f;
	public float playerParagon = 50.0f;

	public enum GameStates
	{
		GameActive,
		GamePaused,
		GameOver,
	}

	public GameStates gameState;

	// Use this for initialization
	void Start ()
	{
		Time.timeScale = 1;
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	void GameStateTracker()
	{
		switch (gameState)
		{
			case GameStates.GameActive:
				Time.timeScale = 1f;
				break;
			case GameStates.GamePaused:
				Time.timeScale = 0f;
				break;
			case GameStates.GameOver:
				break;
		}
	}

	void InputManagement()
	{
		
	}


}
