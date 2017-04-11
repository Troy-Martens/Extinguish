using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCollision : MonoBehaviour {

	public ParticleSystem particleSystem;
	public int initialParticles;

	// Use this for initialization
	void Start () {
		particleSystem = GetComponent<ParticleSystem>();
		initialParticles = particleSystem.maxParticles;
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
	}

	void OnParticleCollision(GameObject other)
	{
		Debug.Log("Col: " + other.gameObject.name);
		if (other.GetComponent<WaterParticleController>())
		{
			particleSystem.maxParticles -= 1;
			Destroy(other);
		}
	}
}
