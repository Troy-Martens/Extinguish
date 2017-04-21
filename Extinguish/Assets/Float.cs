using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour {

	public float floatUpForceScale = 1, floatDownForceThem = 1;
	Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if(collision.gameObject.CompareTag("Water"))
		{
			Vector3 waterPushUsForce = collision.collider.transform.position - transform.position;
			waterPushUsForce = Vector3.Cross(waterPushUsForce.normalized, Vector3.forward);

			GetComponent<Rigidbody2D>().AddForceAtPosition(Vector3.up * floatUpForceScale, collision.contacts[0].point);
			collision.rigidbody.AddForce(waterPushUsForce * floatDownForceThem);

			Debug.DrawRay(transform.position, Vector3.up * floatUpForceScale);
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Water"))
		{
			rb2d.AddForce(Vector2.up * floatUpForceScale);
		}
	}
}
