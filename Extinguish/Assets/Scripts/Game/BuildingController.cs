using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
	#region Variables
	public int paragonValue;
	public int amountOfCashDrops;
	public int cashDropValue = 50;

	public int buildingTotalValue;

	public int amountOfFires = 0;

	public bool isBurning;

	public bool buildingDestroyed = false;
	public bool buildingSaved = false;

	public int fireDamageAmount = 0;

	public float maxBuildingHealth;
	public float currentHealth;
	public float healthPercentage;

	public float smallBuildingHealth = 50;
	public float mediumBuildingHealth = 100;
	public float largeBuildingHealth = 150;


	public FireController[] localFires;

	public GameObject healthBar;
	public Collider2D buildingCollider;

	public GameObject explosionPrefab;


	// Classes
	public GameController gameController;

	// Objects
	public GameObject moneyDropsPrefab;

	public enum BuildingType
	{
		Large,
		Medium,
		Small
	}

	public enum BuildingValue
	{
		Poor,
		Middleclass,
		Upperclass,
		Cute
	}

	public BuildingType buildingType;
	public BuildingValue buildingValue;
	#endregion

	void Awake()
	{
		SetBuildingValues();
	}

	// Use this for initialization
	void Start ()
	{

		gameController = FindObjectOfType<GameController>();

		currentHealth = maxBuildingHealth;

		isBurning = true;

		InstantiateFires();
		InvokeRepeating("FireDamage", 1f, 1f);
		amountOfCashDrops = buildingTotalValue / cashDropValue;

	}

	// Update is called once per frame
	void Update ()
	{
		OnBuildingDestruction();
		UpdateBuildingHUD();
	}

	void SetBuildingValues()
	{
		// Set building monetary values.
		// Set building health values.
		switch (buildingType)
		{
			case BuildingType.Large:
				maxBuildingHealth = largeBuildingHealth;
				Debug.Log("!1");
				break;
			case BuildingType.Medium:
				maxBuildingHealth = mediumBuildingHealth;
				Debug.Log("!12");
				break;
			case BuildingType.Small:
				maxBuildingHealth = smallBuildingHealth;
				Debug.Log("!13");
				break;
		}

		switch (buildingValue)
		{
			case BuildingValue.Cute:
				buildingTotalValue = 50;
				break;
			case BuildingValue.Upperclass:
				buildingTotalValue = 500;
				break;
			case BuildingValue.Middleclass:
				buildingTotalValue = 250;
				break;
			case BuildingValue.Poor:
				buildingTotalValue = 100;
				break;
		}
	}

	void FireDamage()
	{
		if (isBurning && currentHealth > 0)
		currentHealth -= fireDamageAmount;
	}

	void OnBuildingDestruction()
	{
		if (currentHealth <= 0)
			buildingDestroyed = true;
		if (amountOfFires <= 0)
			buildingSaved = true;

		if (buildingDestroyed)
		{
			for (int i = 0; i < amountOfCashDrops; i++)
			{
				GameObject moneyClone = Instantiate(moneyDropsPrefab, this.transform.position, this.transform.rotation);
				Debug.Log(i);
			}

			gameController.currentParagon -= paragonValue;
			Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);
			gameController.buildingsSolved += 1;
			this.gameObject.SetActive(false);
			// Play destruction audio.
			// Destroy game object. Instantiate destroy particle effect.
		}

		if (buildingSaved)
		{
			gameController.currentParagon += paragonValue;
			gameController.buildingsSolved += 1;
			enabled = false;
		}
	}

	void UpdateBuildingHUD()
	{
		if (healthPercentage > 0)
		healthPercentage = (currentHealth / maxBuildingHealth);
		healthBar.transform.localScale = new Vector2(1 * healthPercentage, healthBar.transform.localScale.y);
	}

	void InstantiateFires()
	{
		localFires = GetComponentsInChildren<FireController>();
		for (int i = 0; i < localFires.Length; i++)
		{
			fireDamageAmount += 1;
			amountOfFires += 1;
		}
	}
}
