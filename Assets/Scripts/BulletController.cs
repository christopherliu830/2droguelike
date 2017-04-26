using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public float speed;
	public float damagePerShot;
	public float range;
	public LayerMask shootableLayers;


	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();

		var rawShootDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		rawShootDirection -= transform.position;	
		Vector2 shootDirection = rawShootDirection;
		rb2d.velocity = shootDirection/shootDirection.magnitude * speed;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		var other = HitDetection(rb2d);
		if (other)
		{
			ApplyDamage(damagePerShot, other.collider);
			transform.position = other.point;
			transform.DetachChildren(); //get rid of particles of bullet
			Destroy(gameObject); //destroy bullet
		}
	}

	void ApplyDamage (float damage, Collider2D target)
	{
		EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
		if (enemyHealth)
		{
			enemyHealth.TakeDamage(damage);
		}

	}

	RaycastHit2D HitDetection(Rigidbody2D rb)
	{
		var oldPosition = transform.position;
		Vector3 distanceinFrame = rb.velocity * Time.deltaTime;
		var testPosition = transform.position + distanceinFrame;
		var raycast = Physics2D.Raycast(oldPosition, distanceinFrame, distanceinFrame.magnitude, shootableLayers);
		return raycast;

	}

}
