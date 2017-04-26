using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 4;
	public float speedLimit = 17;
	public float speedDecay;

	private bool facingRight = true;

	public LayerMask mask = -1;
	public float jumpForce = 20;
	public float glideForce = 2;
	private float jumpGrace = 12;
	private float distToGround;
	private bool canJump;

	private Rigidbody2D rb;

	public Transform firePoint;
	public GameObject bullet;

	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		distToGround = GetComponent<BoxCollider2D>().bounds.extents.y;
		canJump = true;
	}
	
	bool IsGrounded()
	{
		return Physics2D.Raycast(transform.position, Vector3.down, (float) (distToGround+0.1), mask.value);
	}

	void Jump ()
	{
		var jumpPressed = Input.GetKey("space");


		if (jumpGrace > 0 && jumpPressed && IsGrounded())
		{
			rb.velocity += (new Vector2(0.0f,jumpForce));
			jumpGrace = 2;
		}
		if (jumpPressed)
		{
			rb.velocity += (new Vector2(0.0f, glideForce));
			jumpGrace--;
		}
		else jumpGrace = 2000;
	}
	
	void Flip()
	{
		facingRight = !facingRight;
		transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
	}
	void Move()
	{
		float moveX = Input.GetAxis("Horizontal");
		var movement = new Vector2(moveX, 0.0f);
		
		if (moveX < 0 && facingRight) Flip();
		if (moveX > 0 && !facingRight) Flip();	

		if (-0.7f <= moveX && moveX <= 0.7f) rb.velocity = new Vector2(rb.velocity.x/(speedDecay+1), rb.velocity.y);
		if (Mathf.Abs(rb.velocity.x) <= speedLimit) rb.velocity += movement * speed; 
		if (Mathf.Abs(rb.velocity.x) >= speedLimit) rb.velocity = new Vector2(Mathf.Abs(moveX)/moveX*speedLimit,rb.velocity.y);
	
	}

	void Shoot()
	{
			
			var rotateTo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			rotateTo -= firePoint.position;

			//firePoint.eulerAngles = (new Vector3(0.0f, 0.0f, Mathf.Atan2(rotateTo.y, rotateTo.x) * Mathf.Rad2Deg));
			firePoint.rotation = Quaternion.identity;

		       	var bulletInstance = Instantiate(bullet, firePoint.position, firePoint.rotation) as GameObject;
			bulletInstance.transform.RotateAround(firePoint.position, Vector3.forward, Mathf.Atan2(rotateTo.y, rotateTo.x) * Mathf.Rad2Deg);
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Shoot();
		}
	}
	void FixedUpdate () 
	{
	
		Move();
		Jump();


		


	}
}

