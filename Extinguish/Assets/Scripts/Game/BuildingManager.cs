using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{

	public float globalCasualtyMax;
	public float globalCasualtyPercentage;

	public float currentGlobalHealth;
	public float maximumHealthLossThreshold = 20f;


	public BuildingController[] buildings;
	public GameObject moneyDropPrefab;

	// Use this for initialization
	void Start ()
	{
		FindBuildings();
	}
	
	// Update is called once per frame
	void Update ()
	{
		BuildingOverview();
	}

	void BuildingOverview()
	{
		// Update global health;
		float currentHealth = 1;
		for (int i = 0; i < buildings.Length; i++)
		{
			currentHealth += buildings[i].currentHealth;
		}

		currentGlobalHealth = currentHealth;

	}

	void FindBuildings()
	{
		buildings = FindObjectsOfType<BuildingController>();

		for (int i = 0; i < buildings.Length; i++)
		{
			Debug.Log(i);
			globalCasualtyMax += buildings[i].maxBuildingHealth;
			Debug.Log("B" + buildings[i].maxBuildingHealth);

			buildings[i].moneyDropsPrefab = moneyDropPrefab;
		}

	}
	
	void UpdateBuildings()
	{
		buildings = FindObjectsOfType<BuildingController>();
	}

}
