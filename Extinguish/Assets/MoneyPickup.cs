using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickup : MonoBehaviour {

	public GameController gameController;
	int cashDropValue = 50;
	public Rigidbody2D rb2d;

	// Use this for initialization
	void Awake ()
	{
		gameController = FindObjectOfType<GameController>();
	}
	
	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D>();
		rb2d.AddForce(new Vector2(Random.Range(-100,100), Random.Range(-100,100)) * 5);
	}
	// Update is called once per frame
	void Update ()
	{
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<PlayerController>() != null)
		{
			gameController.currentMoney += cashDropValue;
			Destroy(this.gameObject);
		}
	}
}
