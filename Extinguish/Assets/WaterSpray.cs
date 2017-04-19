using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpray : MonoBehaviour {

	float sprayReset;
	public GameObject waterParticlePrefab;
	public GameObject spawnLocation;

	public float waterForce = 30.0f;

	// Use this for initialization
	void Start ()
	{
		InvokeRepeating("SprayWater", 0f, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{

	}

	void SprayWater()
	{
		GameObject waterClone = Instantiate(waterParticlePrefab, this.gameObject.transform.position, this.gameObject.transform.rotation);
		waterClone.GetComponent<Rigidbody2D>().AddForce(transform.up * waterForce);
		Destroy(waterClone, 5.0f);
	}
}
