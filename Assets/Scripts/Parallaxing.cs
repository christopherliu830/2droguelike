using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

	public Transform[] backgrounds;
	private float[] parallaxScales;
	public float smoothing = 1f;

	private Transform camera;
	private Vector3 preCamPos;

	// Use this for initialization
	void Start ()
       	{
		camera = Camera.main.transform;
		preCamPos = camera.position;

		parallaxScales = new float[backgrounds.Length];

		for (int i = 0; i < backgrounds.Length; i++)
		{
			parallaxScales[i] = backgrounds[i].position.z*-1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < backgrounds.Length; i++)
		{
			float parallax = (preCamPos.x - camera.position.x) * parallaxScales[i];
			
			float backgroundTargetPosX = backgrounds[i].position.x + parallax;

			var backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}
		preCamPos = camera.position;
	}
}
