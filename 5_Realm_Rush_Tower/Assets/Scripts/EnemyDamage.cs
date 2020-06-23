using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {

	[SerializeField] int hitPoints = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	private void OnParticleCollision(GameObject other)
	{
		ProcessHit();

		if (hitPoints <= 0)
		{
			KillEnemy();
		}
	}

	void ProcessHit ()
	{
		hitPoints -= 1; // TODO - make hit points variable per tower
		print("Current hit points: " + hitPoints);
	}


	private void KillEnemy()
	{
		Destroy(gameObject);
	}
}
