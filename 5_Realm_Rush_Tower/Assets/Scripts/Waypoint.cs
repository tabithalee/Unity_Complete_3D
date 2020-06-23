using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

	[SerializeField] Color exploredColor;

	// public ok as is a data class
	public bool isExplored = false;
	public Waypoint exploredFrom;

	Vector2Int gridPos;
	
	const int gridSize = 10;
	
	/*
	void Update()
	{
		if (isExplored)
		{
			SetTopColor(Color.blue);
		}
	}
	*/

	public int GetGridSize()
	{
		return gridSize;
	}

	public Vector2Int GetGridPos()
	{
		return new Vector2Int(
			Mathf.RoundToInt(transform.position.x / gridSize),
			Mathf.RoundToInt(transform.position.z / gridSize)
		);
	}

	public void SetTopColor(Color color)
	{
		MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
		topMeshRenderer.material.color = color;

	}
}
