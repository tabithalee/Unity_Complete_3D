﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour 
{
	[Range(0.1f, 120f)]
	[SerializeField] float secondsBetweenSpawns = 4f;
	[SerializeField] EnemyMovement enemyPrefab;
	[SerializeField] Transform enemyParentTransform;
	[SerializeField] Text enemiesSpawned;
	[SerializeField] AudioClip spawnedEnemySFX;

	int score = 0;

	// Use this for initialization
	void Start () {
		StartCoroutine(RepeatedlySpawnEnemies());
		enemiesSpawned.text = "Spawned: " + score.ToString();
	}

	IEnumerator RepeatedlySpawnEnemies()
	{
		while (true)
		{
			AddScore();
			GetComponent<AudioSource>().PlayOneShot(spawnedEnemySFX);
			var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
			newEnemy.transform.parent = enemyParentTransform;

			yield return new WaitForSeconds(secondsBetweenSpawns);
		}

	}

	private void AddScore()
	{
		score++;
		enemiesSpawned.text = "Spawned: " + score.ToString();
	}
}
