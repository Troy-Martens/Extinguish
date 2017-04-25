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
	public List<Slider> buildingUITrackers;
	public GameObject buildingUITrackerPrefab;
	public GameObject buildingUIPanel;

	public Slider globalHealth;
	public Slider paragonSlider;

	public VerticalLayoutGroup buildingTracker;
	public List<BuildingController> buildings;

	public bool fadeOut = false;
	public bool fadeIn = false;
	public Image uiImage;

	public float yOffset = 2;

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
		fadeIn = false;
		fadeOut = true;

		slider = GetComponentInChildren<Slider>();
		playerController = FindObjectOfType<PlayerController>();
		gameController = FindObjectOfType<GameController>();
		buildingManager = FindObjectOfType<BuildingManager>();

		globalHealth.maxValue = buildingManager.globalCasualtyMax;
		hudState = HUDState.Active;
		gameController.hudController = this;
		AddBuildingsToTracker();
	}

	// Update is called once per frame
	void Update ()
	{
		paragonSlider.value = gameController.currentParagon;
		

		FadeInOut();
		TrackBuildingStats();
	}

	public void ToggleMenus()
	{
		if (hudState == HUDState.Active)
		{
			pauseMenu.SetActive(false);
			activeMenu.SetActive(true);
			gameController.gameState = GameController.GameStates.GameActive;
			Debug.Log("HUD ACTIVE");	
		}

		if (hudState == HUDState.Paused)
		{
			pauseMenu.SetActive(true);
			activeMenu.SetActive(false);
			gameController.gameState = GameController.GameStates.GamePaused;
			Debug.Log("PAUSE ACTIVE");
		}
	}

	void AddBuildingsToTracker()
	{
		buildings.AddRange(FindObjectsOfType<BuildingController>());

		for (int i = 0; i < buildings.Count; i++)
		{
			GameObject trackerClone = Instantiate(buildingUITrackerPrefab, buildingUIPanel.transform);
			buildingUITrackers.Add(trackerClone.GetComponent<Slider>());
		}
	}

	void TrackBuildingStats()
	{
		globalHealth.value = buildingManager.currentGlobalHealth;

		for (int i = 0; i < buildings.Count; i++)
		{
			buildingUITrackers[i].maxValue = 1;
			buildingUITrackers[i].value = (buildings[i].currentHealth / buildings[i].maxBuildingHealth) * 1;
		}
	}

	public void OnClick_ResumeButton()
	{
		hudState = HUDState.Active;
		ToggleMenus();
	}

	public void OnClick_ExitButton()
	{
		Application.Quit();
	}


	public void OnClick_ReloadLevel()
	{
		gameController.ReloadCurrentLevel();
	}

	void UpdateHUD()
	{
	}

	void FadeInOut()
	{
		if (fadeOut)
		{
			uiImage.canvasRenderer.SetAlpha(1f);
			uiImage.CrossFadeAlpha(0f, 1, false);
			fadeOut = false;
			fadeIn = true;
		}
	}
}
