using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{

	public ParticleSystem particleSystem;
	public BuildingController parentBuilding;

	public int initialParticles;

	// Use this for initialization
	void Start ()
	{
		particleSystem = GetComponent<ParticleSystem>();
		initialParticles = particleSystem.maxParticles;
		parentBuilding = GetComponentInParent<BuildingController>();
		InvokeRepeating("RestoreFlame", 1.0f, 1.0f);
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	void FixedUpdate()
	{
		
	}

	void RestoreFlame()
	{
		if (particleSystem.maxParticles < initialParticles && particleSystem.maxParticles != 0)
		particleSystem.maxParticles += 1;
		if (particleSystem.maxParticles <= 0)
		{
			// Building is safe.
			Destroy(this.gameObject);
		}
	}

	void OnParticleCollision(GameObject other)
	{
		Debug.Log("Col: " + other.gameObject.name);
		if (other.gameObject.CompareTag("Water"))
		{
			particleSystem.maxParticles -= 1;
			Destroy(other.gameObject);
		}

		if (other.gameObject.GetComponent<PlayerController>() != null)
		{
			other.gameObject.GetComponent<PlayerController>().playerState = PlayerController.PlayerStates.Dead;
		}
	}
}
