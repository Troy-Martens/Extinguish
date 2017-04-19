using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{

	public Slider slider;
	public Text winText;
	public Text loseText;

	public bool fireOut = false;

	public PlayerController playerController;
	public GameController gameController;

	public GameObject pauseMenu;
	public GameObject hudObject;

	public bool isActive = false;

	// Use this for initialization
	void Start ()
	{
		slider = GetComponentInChildren<Slider>();
		playerController = FindObjectOfType<PlayerController>();
		gameController = FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		slider.value = playerController.waterTankTime;
		TWinOrLose();
		ToggleMenus();
	}

	void TWinOrLose()
	{
		if (playerController.waterTankTime <= 0)
		{
			loseText.gameObject.SetActive(true);
			Time.timeScale = 0;
		}
		else if (fireOut)
		{
			winText.gameObject.SetActive(true);
			Time.timeScale = 0;
		}

	}

	void ToggleMenus()
	{
		if (Input.GetButton("Start") && !isActive)
		{
			hudObject.SetActive(false);
			pauseMenu.SetActive(true);
			isActive = true;
		}

		else if (Input.GetButton("Start") && isActive)
		{
			hudObject.SetActive(true);
			pauseMenu.SetActive(false);
			isActive = true;
		}
	}
}
