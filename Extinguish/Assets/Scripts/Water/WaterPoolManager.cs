using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPoolManager : MonoBehaviour
{

	public Vector3 position;
	public Vector2 velocity;
	public Vector3 externalForce;

	public float bondingDistance;
	public float moleculeDistance;
	public float bondStrength = 10.0f;

	public float lastDistance = 0f;

	public float mass;
	public float density;
	public float pressure;
	public Color colour;
	public float force;

	public List<GameObject> waterPool;
	public WaterParticleController[] waterParticles;


	public float viscosity;

	// For Pi: Av/Ati = Ai^pressure + Ai^viscosity + Ai^gravity + Aui^external
	// move Pj using Aj and trig_t using Euler step.


	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	void FixedUpdate()
	{
	}



	void SetViscosity()
	{
		// Adjust moleculeDistance and bondStrength based on viscosity?
	}

	/** Pseudo
	 * Gather Pools:
	 * Check distance between all waterParticles. If distance less than bondingDistance, parent object?
	 * Set waterPool GameObject to middle point of all waterParticles in pool.
	 * For pool, calculate middle point between all 
	 * Metaballs for visuals.
	 * */

}
