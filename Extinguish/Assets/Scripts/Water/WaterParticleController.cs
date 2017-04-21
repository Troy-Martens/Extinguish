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
		Debug.Log("Making child collider a trigger");
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

	//void OnTriggerEnter2D(Collider2D other)
	//{
	//	if (other.gameObject.GetComponent<WaterParticleController>() != null)
	//	{
	//		if (other.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude != 0)
	//		{
	//			Rigidbody2D otherRB2D = other.GetComponent<Rigidbody2D>();
	//			rb2d.velocity = otherRB2D.velocity / 2;
	//		}
	//
	//	}
	//}
	//
	//void OnTriggerStay2D(Collider2D other)
	//{
	//	if (other.gameObject.GetComponent<WaterParticleController>() != null)
	//	{
	//
	//		Rigidbody2D otherRB2D = other.GetComponent<Rigidbody2D>();
	//		otherRB2D.AddForce(Vector2.down * 2);
	//	}
	//}

	//oid OnTriggerStay2D(Collider2D other)
	//
	//	if (other.gameObject.GetComponent<Float>() != null)
	//	{
	//		if (waterState == WaterState.Buoyant)
	//		other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * buoyancyForce);
	//	}
	//
}
