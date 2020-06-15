using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

	[SerializeField] Waypoint startWaypoint, endWaypoint;

	Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

	// Use this for initialization
	void Start () {
		LoadBlocks();
		ColorStartAndEnd();
	}

	private void LoadBlocks()
    {
		var waypoints = FindObjectsOfType<Waypoint>();
		foreach (Waypoint waypoint in waypoints)
		{
			var gridPos = waypoint.GetGridPos();
			if (grid.ContainsKey(gridPos))
			{
				Debug.LogWarning("Skipping overlapping block " + waypoint);
			}
			else
			{
				// add to dictionary
				grid.Add(gridPos, waypoint);
			}
		}
    }


	private void ColorStartAndEnd()
	{
		startWaypoint.SetTopColor(Color.green);
		endWaypoint.SetTopColor(Color.red);
	}
}
