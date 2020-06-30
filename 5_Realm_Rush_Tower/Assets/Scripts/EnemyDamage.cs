using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {

	[SerializeField] int hitPoints = 10;
	[SerializeField] ParticleSystem hitParticlePrefab;
	[SerializeField] ParticleSystem deathParticlePrefab;

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
		hitParticlePrefab.Play();
	}


	private void KillEnemy()
	{
		var vfx = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
		vfx.Play(); //TODO - write a co-routine to destroy vfx
		Destroy(gameObject);
	}
}
