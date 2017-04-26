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

	public int buildingsSolved;

	public bool playerWon;
	public bool playerLost;

	bool isEvil = false;

	public HUDController hudController;
	public BuildingManager buildingManager;

	public enum GameStates
	{
		GameActive,
		GamePaused,
		GameOver,
		LevelComplete
	}

	public GameStates gameState;

	void Awake()
	{
		hudController = FindObjectOfType<HUDController>();
		PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);

	}

	// Use this for initialization
	void Start ()
	{
		Time.timeScale = 1;
		gameState = GameStates.GameActive;
		SceneManager.LoadSceneAsync(Scenes.HUD, LoadSceneMode.Additive);
		buildingManager = FindObjectOfType<BuildingManager>();

		PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);


		if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName(Scenes.LevelOne))
		{
			Debug.Log("Level One Loaded");
			PlayerPrefs.SetInt("Paragon", 50);
			PlayerPrefs.SetInt("Money", 0);
		}

		currentParagon = PlayerPrefs.GetInt("Paragon");
		currentMoney = PlayerPrefs.GetInt("Money");
	}

	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			ReloadCurrentLevel();
		}

		Mathf.Clamp(currentParagon, 0, 100);

		GameStateTracker();
		InputManagement();

	}

	void GameStateTracker()
	{
		switch (gameState)
		{
			case GameStates.GameActive:
				Time.timeScale = 1f;

				if (buildingsSolved >= buildingManager.buildings.Length)
				{
					playerWon = true;
				} 

				break;
			case GameStates.GamePaused:
				Time.timeScale = 0f;
				break;
			case GameStates.GameOver:
				playerLost = true;
				Invoke("LoadNextLevel", 1.0f);
				break;
			case GameStates.LevelComplete:
				playerWon = true;
				LoadNextLevel();
				break;
		}
	}

	void InputManagement()
	{
		if (Input.GetButtonDown("Start") && gameState != GameStates.GamePaused)
		{
			hudController.hudState = HUDController.HUDState.Paused;
			hudController.ToggleMenus();
		}

		else if (Input.GetButtonDown("Start") && gameState == GameStates.GamePaused)
		{
			hudController.hudState = HUDController.HUDState.Active;
			hudController.ToggleMenus();
		}
	}

	void LoadNextLevel()
	{
		
		if (playerLost)
		{
			// Load game over ("Replay? Quit?")
			PlayerPrefs.SetString("GameOverCondition", "Lost");
			SceneManager.LoadScene(Scenes.GameOver);
		}

		else if (playerWon)
		{
			if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName(Scenes.LevelOne))
			{
				Debug.Log("W1");
				PlayerPrefs.SetInt("Paragon", currentParagon);
				PlayerPrefs.SetInt("Money", currentMoney);
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
				enabled = false;
			}

			else if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName(Scenes.LevelOne) && SceneManager.GetActiveScene() != SceneManager.GetSceneByName(Scenes.LevelThree))
			{
				Debug.Log("W2");
				PlayerPrefs.SetInt("Paragon", (PlayerPrefs.GetInt("Paragon") + currentParagon));
				PlayerPrefs.SetInt("Money", (PlayerPrefs.GetInt("Money") + currentParagon));
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			}

			else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName(Scenes.LevelThree))
			{
				Debug.Log("W3");
				PlayerPrefs.SetInt("Paragon", (PlayerPrefs.GetInt("Paragon") + currentParagon));
				PlayerPrefs.SetInt("Money", (PlayerPrefs.GetInt("Money") + currentParagon));
				SceneManager.LoadScene(Scenes.WinScreen);
			}
		}

	}

	public void ReloadCurrentLevel()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

}
