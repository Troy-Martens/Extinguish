using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour
{


	public Transform[] backgrounds;
	private float[] parallaxScales;
	public float smoothing = 1f;

	private Transform cam;
	private Vector3 previousCamPos;


	void Awake()
	{
		cam = Camera.main.transform;
	}

	void Start()
	{
		previousCamPos = cam.position;
		parallaxScales = new float[backgrounds.Length];

		for (int i = 0; i < backgrounds.Length; i++)
		{
			parallaxScales[i] = backgrounds[i].position.z * -1;

		}
	}

	void Update()
	{

		for (int i = 0; i < backgrounds.Length; i++)
		{

			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
			float parallaxy = (previousCamPos.y - cam.position.y) * parallaxScales[i];


			float backgroundTargetPosX = backgrounds[i].position.x + parallax;
			float backgroundTargetPoxY = backgrounds[i].position.y + parallaxy;


			Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgroundTargetPoxY, backgrounds[i].position.z);


			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);

		}

		previousCamPos = cam.position;
	}
}