﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	#region Class Variables
	public float playerSpeed = 5.0f;
	public float jumpForce = 10.0f;
	public float maxSpeed = 5f;
	public float waterSprayDelay = 0.1f;

	public float horizontalInput;
	public float verticalInput;

	public float sprayForce = 5.0f;

	public int sprayParticleCount = 1;

	public bool grounded = false;
	public bool jump = true;
	public bool facingRight;

	public bool shoot;
	public float waterTankTime;
	public float waterTankTimeBase = 10f;

	public GameObject groundCheck;
	public Transform rotatingArm;

	public LayerMask environmentCheck;

	public Animator animator;
	public Rigidbody2D rb2d;
	public GameController gameController;

	public GameObject waterParticlePrefab;
	public GameObject barrelPosition;

	public GameObject waterBar;

	public bool sprayReset;
	public float sprayDelayB = 0.5f;
	float sprayDelayC;

	float defaultMass;

	public float drownTimerBase = 5f;
	public float drownTimerCurrent;

	public bool isDrowning = false;

	public Image waterFill;

	#endregion

	public enum PlayerStates
	{
		Idle,
		Walking,
		Extinguishing,
		Jumping,
		Dead,
		Falling
	}

	public PlayerStates playerState;

	#region Unity Functions
	// Use this for initialization
	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		sprayDelayC = sprayDelayB;

		defaultMass = rb2d.mass;
		drownTimerCurrent = drownTimerBase;
		gameController = FindObjectOfType<GameController>();
		waterTankTime = waterTankTimeBase;
	}


	// Update is called once per frame
	void Update ()
	{
		// Update variables
		horizontalInput = Input.GetAxis("Horizontal");
		verticalInput = Input.GetAxis("Vertical");
		//grounded = Physics2D.Linecast(transform.position, groundCheck.transform.position, 1 << LayerMask.NameToLayer("Environment"));

		if (Physics2D.Linecast(transform.position, groundCheck.transform.position, 1 << LayerMask.NameToLayer("Environment")) || Physics2D.Linecast(transform.position, groundCheck.transform.position, 1 << LayerMask.NameToLayer("Interactable")))
			grounded = true;
		else
			grounded = false;

		// Jump check.
		if (verticalInput > 0.5 && grounded && playerState != PlayerStates.Extinguishing)
		{
			jump = true;
		}
		else
		{
			jump = false;
		}

		// Spray fire rate.
		if (!sprayReset && sprayDelayC > 0)
		{
			sprayDelayC -= Time.deltaTime;
		}
		if (sprayDelayC <= 0)
		{
			sprayDelayC = sprayDelayB;
			sprayReset = true;
		}


		// Functions
		SetAnimation();
		PlayerStateManager();
		DrownTracker();
		PlayerStateManager();
		UpdateHUD();
	}

	void FixedUpdate()
	{
		PlayerMovement();
	}

	void LateUpdate()
	{
		if (playerState == PlayerStates.Extinguishing)
		{
			rotatingArm.Rotate(0f, 0f, verticalInput * 50f);
		}
	}
	#endregion

	void FlipPlayer()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale *= -1;
		transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
	}

	void SetAnimation()
	{
		animator.SetInteger("PlayerState", (int)playerState);
		if (rb2d.velocity.magnitude < 0.1f && playerState != PlayerStates.Extinguishing)
		{
			playerState = PlayerStates.Idle;
		}

		if (!grounded && rb2d.velocity.y < 0)
		{
			playerState = PlayerStates.Falling;
		}
	}

	void PlayerMovement()
	{

		if (horizontalInput < 0 && !facingRight)
			FlipPlayer();
		else if (horizontalInput > 0 && facingRight)
			FlipPlayer();

		if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
			rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);



		if (Input.GetAxis("Horizontal") > 0 && playerState != PlayerStates.Extinguishing)
		{
			rb2d.AddForce(transform.right * playerSpeed * horizontalInput);
			if (rb2d.velocity.magnitude > 0.1f && grounded)
			playerState = PlayerStates.Walking;
		}

		if (Input.GetAxis("Horizontal") < 0 && playerState != PlayerStates.Extinguishing) 
		{
			rb2d.AddForce(transform.right * horizontalInput * playerSpeed);
			if (rb2d.velocity.magnitude > 0.1f && grounded)
			playerState = PlayerStates.Walking;
		}

		if (Input.GetButton("Extinguish") && grounded && playerState != (PlayerStates.Falling | PlayerStates.Jumping))
		{
			playerState = PlayerStates.Extinguishing;
			//rb2d.mass = 500;
			//rb2d.velocity = new Vector3(0,0,0);



			// Spawn water;
			PlayerSpray();

		}

		else if (rb2d.velocity.magnitude < 0.1f && grounded)
		{
			playerState = PlayerStates.Idle;
		}

		if (jump && playerState != PlayerStates.Extinguishing)
		{
			Debug.Log("Jump");
			rb2d.AddForce(new Vector2(0f, jumpForce));
			playerState = PlayerStates.Jumping;
		}
	}


	void PlayerStateManager()
	{
		switch (playerState)
		{
			case PlayerStates.Dead:
				gameController.gameState = GameController.GameStates.GameOver;
				//enabled = false;
				break;
			case PlayerStates.Extinguishing:
				break;
			case PlayerStates.Walking:
				break;
			case PlayerStates.Idle:
				break;
			case PlayerStates.Jumping:
				break;
		}
	}

	void PlayerSpray()
	{
		if (sprayReset)
		{
			if (horizontalInput > 0 && waterTankTime > 0)
			{
				InstantiateWaterSpray(false, 0.05f);
				sprayReset = false;
				waterTankTime -= Time.deltaTime;

			}
			else if (horizontalInput < 0 && waterTankTime > 0)
			{
				InstantiateWaterSpray(true, 0.05f);
				sprayReset = false;
				waterTankTime -= Time.deltaTime;
			}
		}
	}

	void DrownTracker()
	{
		if (isDrowning)
		{
			drownTimerCurrent -= Time.deltaTime;
			// Set drowning audio active.
		}
		else
		{
			drownTimerCurrent = drownTimerBase;
			// Set drowning audio inactive.
		}

		if (drownTimerCurrent <= 0)
		{
			playerState = PlayerStates.Dead;
		}
	}

	void InstantiateWaterSpray(bool reversed, float offsetAmount)
	{
		float offset = 0;
		for (int i = 0; i < sprayParticleCount; i++)
		{
			if (!reversed)
			{
				GameObject waterParticleClone = Instantiate(waterParticlePrefab, new Vector2(barrelPosition.transform.position.x, (barrelPosition.transform.position.y + offset)), barrelPosition.transform.rotation);
				waterParticleClone.GetComponent<Rigidbody2D>().AddForce(barrelPosition.transform.right * sprayForce);
			}
			else
			{
				//GameObject waterParticleClone = Instantiate(waterParticlePrefab, barrelPosition.transform.position, barrelPosition.transform.rotation);
				GameObject waterParticleClone = Instantiate(waterParticlePrefab, new Vector2(barrelPosition.transform.position.x, (barrelPosition.transform.position.y + offset)), barrelPosition.transform.rotation);
				waterParticleClone.GetComponent<Rigidbody2D>().AddForce(barrelPosition.transform.right * sprayForce * -1);
			}
			offset += offsetAmount;
		}
	}

	void UpdateHUD()
	{
		// Fuck my life, I really shouldn't have just flipped the player transform.
		// Fuck my life, why did I want to have an easy to edit additive HUD scene
		// with no contingency for... I just realised I could have just added the
		// water bar into the HUD scene and manually set it's position to the player + y offset...
		// Fuck... fuckity fuck fuck. Fuck.

		waterBar.transform.localScale = new Vector2((1 * (waterTankTime / waterTankTimeBase)), waterBar.transform.localScale.y);
		if (transform.localScale.x < 0)
		{
			waterFill.rectTransform.anchorMin = new Vector2(1, 0.5f);
			waterFill.rectTransform.anchorMax = new Vector2(1, 0.5f);
			waterFill.rectTransform.pivot = new Vector2(1, 0.5f);
		}
		else
		{
			waterFill.rectTransform.anchorMin = new Vector2(0, 0.5f);
			waterFill.rectTransform.anchorMax = new Vector2(0, 0.5f);
			waterFill.rectTransform.pivot = new Vector2(0, 0.5f);
		}
	}
}
