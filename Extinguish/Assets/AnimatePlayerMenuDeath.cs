using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayerMenuDeath : MonoBehaviour {

	Animator animator;
	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator>();
		animator.SetInteger("PlayerState", 4);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
