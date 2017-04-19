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

	public CircleCollider2D proximityCheck;

	public float bondingDistance = 0.2f;
	public float bondStrength = 2f;

	public bool addForce = false;
	public LayerMask waterLayer;



	// Use this for initialization
	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D>();
		proximityCheck = GetComponentInChildren<CircleCollider2D>();
		waterPoolManager = FindObjectOfType<WaterPoolManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	void FixedUpdate()
	{
	}

}
