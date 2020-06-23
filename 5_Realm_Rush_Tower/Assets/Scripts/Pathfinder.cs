using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

	[SerializeField] Waypoint startWaypoint, endWaypoint;

	Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
	Queue<Waypoint> queue = new Queue<Waypoint>();
	bool isRunning = true;
	Waypoint searchCenter; // the current searchCenter
	List<Waypoint> path  = new List<Waypoint>();


	Vector2Int[] directions = {
		Vector2Int.up,
		Vector2Int.right,
		Vector2Int.down,
		Vector2Int.left,
	};

	public List<Waypoint> GetPath()
	{
		if (path.Count == 0)
		{
			CalculatePath();
		}
		return path;
	}

	private void CalculatePath()
	{
		LoadBlocks();
		//ColorStartAndEnd();
		BreadthFirstSearch();
		CreatePath();
	}

	private void CreatePath()
	{
		path.Add(endWaypoint);

		Waypoint previous = endWaypoint.exploredFrom;
		while (previous != startWaypoint)
		{
			path.Add(previous);
			previous = previous.exploredFrom;
		}

		path.Add(startWaypoint);
		path.Reverse();
	}

	private void BreadthFirstSearch()
	{
		queue.Enqueue(startWaypoint);

		while(queue.Count > 0 && isRunning)
		{
			searchCenter = queue.Dequeue();
			searchCenter.isExplored = true;
			HaltIfEndFound();
			ExploreNeighbours();
		}
	}

	private void HaltIfEndFound()
	{
		if (searchCenter == endWaypoint)
		{
			isRunning = false;
		}
	}

	private void ExploreNeighbours()
	{
		if (!isRunning) { return; }

		foreach (Vector2Int direction in directions)
		{
			Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + direction;
			
			if(grid.ContainsKey(neighbourCoordinates))
			{
				QueueNewNeighbours(neighbourCoordinates);
			}
		}
	}

	private void QueueNewNeighbours(Vector2Int neighbourCoordinates)
	{
		Waypoint neighbour = grid[neighbourCoordinates];
		if (!neighbour.isExplored && !queue.Contains(neighbour))
		{
			queue.Enqueue(neighbour);
			neighbour.exploredFrom = searchCenter;
		}
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
		// TODO - consider leaving out
		startWaypoint.SetTopColor(Color.green);
		endWaypoint.SetTopColor(Color.red);
	}
}
