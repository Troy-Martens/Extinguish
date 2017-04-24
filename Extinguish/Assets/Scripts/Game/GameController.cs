using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public float playerMoney = 0f;
	public float playerParagon = 50.0f;

	public int currentMoney = 0;
	public int currentParagon = 50;

	public int currentLevel;

	bool playerWon;
	bool playerLost;

	bool isEvil = false;

	public HUDController hudController;

	public enum GameStates
	{
		GameActive,
		GamePaused,
		GameOver,
		GameLoading
	}

	public GameStates gameState;

	void Awake()
	{
	}

	// Use this for initialization
	void Start ()
	{
		Time.timeScale = 1;
		gameState = GameStates.GameActive;
		SceneManager.LoadSceneAsync(Scenes.HUD, LoadSceneMode.Additive);
		hudController = FindObjectOfType<HUDController>();
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
				LoadNextLevel();
				break;
		}
	}

	void InputManagement()
	{
		if (Input.GetKey("Pause") && gameState != (GameStates.GamePaused | GameStates.GameOver))
		{
			hudController.hudState = HUDController.HUDState.Paused;
			hudController.ToggleMenus();
		}

		if (Input.GetKey("Pause") && gameState != GameStates.GameOver && gameState == GameStates.GamePaused)
		{
			hudController.hudState = HUDController.HUDState.Active;
			hudController.ToggleMenus();
		}
	}

	void LoadNextLevel()
	{
		if (playerWon)
		{
			if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName(Scenes.LevelOne))
			{
				PlayerPrefs.SetInt("Paragon", currentParagon);
				PlayerPrefs.SetInt("Money", currentMoney);
			}

			else
			{
				PlayerPrefs.SetInt("Paragon", (PlayerPrefs.GetInt("Paragon") + currentParagon));
				PlayerPrefs.SetInt("Money", (PlayerPrefs.GetInt("Money") + currentParagon));
			}
		}

		else if (playerLost)
		{
			// Load game over ("Replay? Quit?")
			PlayerPrefs.SetString("GameOverCondition", "Lost");
			SceneManager.LoadScene(Scenes.GameOver);
		}
	}

}
