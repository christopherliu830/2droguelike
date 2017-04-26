using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	
	public float speed;
	public ParticleSystem blood;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == 8) //player
		{
			other.transform.position = (new Vector3(0f,0f,0f));
		}


	}
}
