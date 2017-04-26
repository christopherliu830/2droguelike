using UnityEngine;
using System.Collections;

public class ParticleController : MonoBehaviour {

	private ParticleSystem ps;
	private int timer;

	// Use this for initialization
	void Start () 
	{
		ps = GetComponent<ParticleSystem>();
		timer = 01;
	}
	
	// Update is called once per frame
	void Update () {
		if (ps)
		{
			if (transform.root == transform)
			{
				timer--;
			}
			if (timer <= 0)
			{
				ps.enableEmission = false;
			}
			if (!ps.IsAlive())
			{
				Destroy(gameObject);
			}
		}
	
	}
}
