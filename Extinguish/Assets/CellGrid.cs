using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class CellGrid : MonoBehaviour
{
	public int xdim, ydim;
	public float cellsize;

	public int[,] cells;

	// Use this for initialization
	void Start ()
	{
		cells = new int[xdim, ydim];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void FixedUpdate()
	{
		//zero all out
	}

	public void Inc(Vector3 pos)
	{
		int x = 0, y = 0;

		if (WorldToIndex(pos, ref x, ref y))
		{
			cells[x, y]++;
		}
	}

	public void Dec(Vector3 pos)
	{
		int x = 0, y = 0;

		if (WorldToIndex(pos, ref x, ref y))
		{
			cells[x, y]--;
		}
	}

	public int Cur(Vector3 pos)
	{
		int x = 0, y = 0;

		if(WorldToIndex(pos, ref x, ref y))
		{
			return cells[x, y];
		}

		return 0;
	}

	public bool WorldToIndex(Vector3 pos, ref int x, ref int y)
	{
		var local = transform.InverseTransformPoint(pos);

		local /= cellsize;

		x = Mathf.FloorToInt(local.x);
		y = Mathf.FloorToInt(local.y);

		if (x >= 0 && y >= 0 && x < xdim && y < ydim)
			return true;

		return false;
	}

	void OnDrawGizmos()
	{
		var size = new Vector3(xdim * cellsize, ydim * cellsize, 1);
		Vector3 corner = transform.position;
		Vector3 otherCorner = corner + size;

		var centre = corner + otherCorner;
		centre /= 2;
		Gizmos.DrawWireCube(centre, size);
	}
}
