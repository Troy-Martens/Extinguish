using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticleController : MonoBehaviour
{
	public Rigidbody2D rb2d;
	public bool isInPool = false;
	public int waterPoolIndex;
	public WaterPoolManager waterPoolManager;

	public GameObject closestObject;

	public CircleCollider2D col2D;

	public float bondingDistance = 0.2f;
	public float bondStrength = 2f;

	public bool addForce = false;
	public LayerMask waterLayer;

	public float waterSepForce = 1;

	public float buoyancyForce = 2f;

	public enum WaterState
	{
		Active,
		Buoyant
	}

	public WaterState waterState;

	// Use this for initialization
	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		//col2D = GetComponentInChildren<CircleCollider2D>();
		waterPoolManager = FindObjectOfType<WaterPoolManager>();

		Invoke("FixWaterInteraction", 2.0f);
	}

	void FixWaterInteraction()
	{
		col2D.isTrigger = true;
	}

	// Update is called once per frame
	void Update()
	{
		StateManager();
	}

	void FixedUpdate()
	{
	}

	void StateManager()
	{
		switch (waterState)
		{
			case WaterState.Active:
				break;
			case WaterState.Buoyant:
				break;
		}
	}

	//a list of all water connected to us

	/*
	 * add on enter
	 * remove on exit
	 * 
	 * in fixed update if length > 2 
	 *	push...
	 */

	void OnCollisionStay2D(Collision2D collision)
	{
		if(collision.collider.CompareTag("Water"))//&& transform.position.y > collision.collider.transform.position.y)
		{
			rb2d.AddForce(collision.contacts[0].normal * waterSepForce);
		}
	}
}
