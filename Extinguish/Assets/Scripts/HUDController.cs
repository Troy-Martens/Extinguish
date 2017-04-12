using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

	public Slider slider;
	public Text winText;
	public Text loseText;

	public bool fireOut = false;

	public PlayerController playerController;

	// Use this for initialization
	void Start ()
	{
		slider = GetComponentInChildren<Slider>();
		playerController = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		slider.value = playerController.waterTankTime;
		TWinOrLose();
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
}
