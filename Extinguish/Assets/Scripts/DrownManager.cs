using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrownManager : MonoBehaviour {

	public Collider2D col2d;
	public LayerMask layerMask;
	public PlayerController playerController;

	// Use this for initialization
	void Start () {
		playerController = FindObjectOfType<PlayerController>();
		col2d = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (col2d.IsTouchingLayers(layerMask))
			playerController.isDrowning = true;
		else
			playerController.isDrowning = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
	}

	void OnTriggerExit2D(Collider2D other)
	{
	}
}
