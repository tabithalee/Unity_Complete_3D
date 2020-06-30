using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// ok because there will only be one pathfinder object in this simple world
		Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
		var path = pathfinder.GetPath();

		StartCoroutine(FollowPath(path));
	}

	IEnumerator FollowPath(List<Waypoint> path)
	{
		foreach (Waypoint waypoint in path)
		{
			transform.position = waypoint.transform.position;
			yield return new WaitForSeconds(2f);
		}
	}
}
