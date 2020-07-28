using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {

	public int hitPoints = 10;
	[SerializeField] ParticleSystem hitParticlePrefab;
	[SerializeField] ParticleSystem deathParticlePrefab;
	[SerializeField] AudioClip enemyHitSFX;
	[SerializeField] AudioClip enemyDeathSFX;

	AudioSource myAudioSource;

	private void Start ()
	{
		myAudioSource = GetComponent<AudioSource>();
	}
	
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
		myAudioSource.PlayOneShot(enemyHitSFX);
		hitParticlePrefab.Play();
	}

	public void KillEnemy() // TODO - refactor KillEnemy and SelfDestruct
	{
		var vfx = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
		vfx.Play();
		Destroy(vfx.gameObject, vfx.main.duration);
		AudioSource.PlayClipAtPoint(enemyDeathSFX, Camera.main.transform.position);
		Destroy(gameObject);
	}
}
