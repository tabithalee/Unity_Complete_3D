using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {

	public int hitPoints = 10;
	[SerializeField] ParticleSystem hitParticlePrefab;
	[SerializeField] ParticleSystem deathParticlePrefab;
	
	private void OnParticleCollision(GameObject other)
	{
		ProcessHit(other);

		if (hitPoints <= 0)
		{
			KillEnemy();
		}
	}

	void ProcessHit (GameObject towerBullet)
	{
		hitPoints -= towerBullet.GetComponentInParent<Tower>().damagePoints;
		hitParticlePrefab.Play();
	}


	private void KillEnemy()
	{
		var vfx = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
		vfx.Play();
		Destroy(vfx.gameObject, vfx.main.duration);
		Destroy(gameObject);
	}
}
