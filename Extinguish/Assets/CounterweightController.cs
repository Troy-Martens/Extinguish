using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterweightController : MonoBehaviour {

	public GameObject platform;
	public GameObject counterWeight;

	public int counterWeightMass;
	public int platformMass = 10;

	public float yTargetTop;
	public float yTargetBottom;

	public Rigidbody2D rb2d;

	// Use this for initialization
	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		CounterWeight();
	}

	void CounterWeight()
	{
		if (counterWeightMass > platformMass)
		{
			if (platform.transform.localPosition.y < yTargetTop)
			{
				platform.transform.localPosition = new Vector3(platform.transform.localPosition.x, platform.transform.localPosition.y + 1f * Time.deltaTime);
				counterWeight.transform.localPosition = new Vector3(counterWeight.transform.localPosition.x, counterWeight.transform.localPosition.y - 1f * Time.deltaTime);

			}
		}

		else if (counterWeightMass < platformMass && counterWeight.transform.localPosition.y < yTargetTop)
		{
				platform.transform.localPosition = new Vector3(platform.transform.localPosition.x, platform.transform.localPosition.y - 1f * Time.deltaTime);
				counterWeight.transform.localPosition = new Vector3(counterWeight.transform.localPosition.x, counterWeight.transform.localPosition.y + 1f * Time.deltaTime);
		}

		else if (counterWeightMass == platformMass)
		{

		}
	}




}
