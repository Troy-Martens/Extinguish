using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
	#region Variables
	public Slider slider;
	public Text winText;
	public Text loseText;

	public PlayerController playerController;
	public GameController gameController;
	public BuildingManager buildingManager;

	public GameObject pauseMenu;
	public GameObject activeMenu;
	public GameObject buildingUIPrefab;

	public Slider globalHealth;
	public Slider paragonSlider;

	public VerticalLayoutGroup buildingTracker;
	public BuildingController[] buildings;

	public Button resumeButton;
	public Button exitButton;
	#endregion


	public float[] buildingMaxHealth;

	public bool isActive = false;

	public enum HUDState
	{
		Active,
		Paused
	}

	public HUDState hudState;
	// Use this for initialization
	void Start ()
	{
		slider = GetComponentInChildren<Slider>();
		playerController = FindObjectOfType<PlayerController>();
		gameController = FindObjectOfType<GameController>();
		buildingManager = FindObjectOfType<BuildingManager>();

		globalHealth.maxValue = buildingManager.globalCasualtyMax;
		hudState = HUDState.Active;
	}
	
	// Update is called once per frame
	void Update ()
	{
		slider.value = playerController.waterTankTime;
		paragonSlider.value = gameController.currentParagon;

		AddBuildingsToTracker();
		TrackBuildingStats();
	}

	void TWinOrLose()
	{
		if (playerController.waterTankTime <= 0)
		{
			loseText.gameObject.SetActive(true);
			Time.timeScale = 0;
		}
	}

	public void ToggleMenus()
	{
		if (hudState == HUDState.Active)
		{
			pauseMenu.SetActive(true);
			activeMenu.SetActive(false);
			gameController.gameState = GameController.GameStates.GameActive;	
		}

		if (hudState == HUDState.Paused)
		{
			pauseMenu.SetActive(false);
			activeMenu.SetActive(true);
			gameController.gameState = GameController.GameStates.GamePaused;

		}
	}

	void AddBuildingsToTracker()
	{
		buildings = FindObjectsOfType<BuildingController>();

		for (int i = 0; i < buildings.Length; i++)
		{
		}
	}

	void TrackBuildingStats()
	{
		globalHealth.value = buildingManager.currentGlobalHealth;
	}

	void OnClick_ResumeButton()
	{
		hudState = HUDState.Active;
		ToggleMenus();
	}

	void OnClick_ExitButton()
	{
		Application.Quit();
	}
}
