using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyancy : MonoBehaviour {

	public Rigidbody2D rb2d;
	public bool applyingBuoyancy = false;
	public Collider2D col2D;

	public float buoyancyForce = 5f;


	public LayerMask buoyancyLayers;

	// Use this for initialization
	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D>();
		col2D = GetComponent<Collider2D>();
		buoyancyLayers = 1 << LayerMask.NameToLayer("Water");
	}
	
	// Update is called once per frame
	void Update ()
	{
		//if (col2D.IsTouchingLayers(buoyancyLayers))
		//{
		//	applyingBuoyancy = true;
		//}
		//else
		//	applyingBuoyancy = false;
	}

	void FixedUpdate()
	{
		if (applyingBuoyancy)
			rb2d.AddForce(new Vector2(0, 1 * buoyancyForce));
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.GetComponent<WaterParticleController>() != null)
		{
			applyingBuoyancy = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		applyingBuoyancy = false;
	}
}
