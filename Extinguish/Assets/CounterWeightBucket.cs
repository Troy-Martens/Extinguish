using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterWeightBucket : MonoBehaviour {

	public CounterweightController counterweightController;
	int waterMass = 1;

	// Use this for initialization
	void Start ()
	{
		counterweightController = GetComponentInParent<CounterweightController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<WaterParticleController>() != null)
		{
			counterweightController.counterWeightMass += 1;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		{
			if (other.GetComponent<WaterParticleController>() != null)
			{
				counterweightController.counterWeightMass -= 1;
			}
		}
	}
}
