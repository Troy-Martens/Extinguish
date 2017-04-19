using UnityEngine;
using System.Collections;

public class SmoothCamera2D : MonoBehaviour
{
	public float Damping = 12.0f;
	public Transform Player;
	public float Height = 13.0f;
	public float Offset = 0.0f;
 
	private Vector3 Center;
	float ViewDistance = 3.0f;
 
 void Update()
	{
		var mousePos = Input.mousePosition;
		mousePos.z = ViewDistance;
		Vector3 CursorPosition = Camera.main.ScreenToWorldPoint(mousePos);

		var PlayerPosition = Player.position;

		Center = new Vector3((PlayerPosition.x + CursorPosition.x) / 2, PlayerPosition.y, (PlayerPosition.z + CursorPosition.z) / 2);

		transform.position = Vector3.Lerp(transform.position, Center + new Vector3(0, Height, Offset), Time.deltaTime * Damping);
	}
}