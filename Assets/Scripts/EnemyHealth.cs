using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	
	public float startingHealth = 100f;
	public float currentHealth;

	ParticleSystem hitParticles;
	BoxCollider2D boxCollider;

	bool isDead;

	// Use this for initialization
	void Start () 
	{
		hitParticles = GetComponentInChildren<ParticleSystem>();
		boxCollider = GetComponent<BoxCollider2D>();
		currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void TakeDamage(float amount)
	{
		if(isDead)
		{
			return;
		}

		print ("enemy health 35: taking damage\n");
		currentHealth -= amount;
		print ("currentHealth" + currentHealth);
		//hitParticles.Play();
		
		if (currentHealth <= 0)
		{
			Death();
		}
	}

	void Death()
	{
		isDead = true;
		boxCollider.isTrigger = true;
		Destroy(gameObject);
	}


}
