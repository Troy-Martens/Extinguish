using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float playerSpeed = 5.0f;
	public float jumpForce = 10.0f;
	public float maxSpeed = 5f;
	public float waterSprayDelay = 0.1f;

	public float horizontalInput;
	public float verticalInput;

	public float sprayForce = 5.0f;

	private bool grounded = false;
	public bool jump = true;
	public bool facingRight;

	public bool shoot;
	public float waterTankTime = 10.0f;

	public GameObject groundCheck;
	public Transform rotatingArm;

	public LayerMask environmentCheck;

	public Animator animator;
	public Rigidbody2D rb2d;

	public GameObject waterParticlePrefab;
	public GameObject barrelPosition;

	public bool sprayReset;
	public float sprayDelayB = 0.5f;
	float sprayDelayC;

	float defaultMass;
	


	public enum PlayerStates
	{
		Idle,
		Walking,
		Extinguishing,
		Jumping,
		Dying,
		Falling
	}

	public PlayerStates playerState;


	// Use this for initialization
	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		sprayDelayC = sprayDelayB;

		defaultMass = rb2d.mass;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Update variables
		horizontalInput = Input.GetAxis("Horizontal");
		verticalInput = Input.GetAxis("Vertical");
		grounded = Physics2D.Linecast(transform.position, groundCheck.transform.position, 1 << LayerMask.NameToLayer("Environment"));

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



		if (Input.GetAxis("Horizontal") > 0)
		{
			rb2d.AddForce(transform.right * playerSpeed * horizontalInput);
			if (rb2d.velocity.magnitude > 0.1f && grounded)
			playerState = PlayerStates.Walking;
		}
		if (Input.GetAxis("Horizontal") < 0)
		{
			rb2d.AddForce(transform.right * horizontalInput * playerSpeed);
			if (rb2d.velocity.magnitude > 0.1f && grounded)
			playerState = PlayerStates.Walking;
		}

		if (Input.GetButton("Extinguish") && grounded)
		{
			playerState = PlayerStates.Extinguishing;
			rb2d.mass = 500;
			rb2d.velocity = new Vector3(0,0,0);

			// Spawn water;
			PlayerSpray();

		}
		else
		{
			rb2d.mass = defaultMass;
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
		
	}

	void PlayerSpray()
	{
		if (sprayReset)
		{
			if (horizontalInput > 0 && waterTankTime > 0)
			{
				GameObject waterParticleClone = Instantiate(waterParticlePrefab, barrelPosition.transform.position, barrelPosition.transform.rotation);
				waterParticleClone.GetComponent<Rigidbody2D>().AddForce(barrelPosition.transform.right * sprayForce);
				sprayReset = false;
				waterTankTime -= Time.deltaTime;
			}
			else if (horizontalInput < 0 && waterTankTime > 0)
			{
				GameObject waterParticleClone = Instantiate(waterParticlePrefab, barrelPosition.transform.position, barrelPosition.transform.rotation);
				waterParticleClone.GetComponent<Rigidbody2D>().AddForce(barrelPosition.transform.right * sprayForce * -1);
				sprayReset = false;
				waterTankTime -= Time.deltaTime;
			}
		}

		//Debug.Log(horizontalInput);
	}


}
