using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float playerSpeed = 5.0f;
	public float jumpForce = 10.0f;
	public float maxSpeed = 5f;

	public float horizontalInput;
	public float verticalInput;

	public float sprayForce;

	private bool grounded = false;
	public bool jump = true;
	public bool facingRight;
	public GameObject groundCheck;
	public Transform rotatingArm;

	public LayerMask environmentCheck;

	public Animator animator;
	public Rigidbody2D rb2d;
	


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
	}
	
	// Update is called once per frame
	void Update ()
	{
		horizontalInput = Input.GetAxis("Horizontal");
		verticalInput = Input.GetAxis("Vertical");

		grounded = Physics2D.Linecast(transform.position, groundCheck.transform.position, 1 << LayerMask.NameToLayer("Environment"));

		PlayerStateManager();

		if (verticalInput > 0.5 && grounded && playerState != PlayerStates.Extinguishing)
		{
			jump = true;
		}

		else
		{
			jump = false;
		}

		SetAnimation();
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
			//rotatingArm.transform.eulerAngles = new Vector3(rotatingArm.eulerAngles.x, rotatingArm.eulerAngles.y, Mathf.Atan2(horizontalInput, verticalInput));
			Debug.Log("ROTATING!");
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
			rb2d.velocity = new Vector3(0,0,0);

			// Spawn water;
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


}
