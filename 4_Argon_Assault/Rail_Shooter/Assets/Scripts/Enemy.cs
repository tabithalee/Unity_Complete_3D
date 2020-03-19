﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField] GameObject deathFX;
	[SerializeField] Transform parent;
	[SerializeField] int scorePerHit = 12;
	[SerializeField] int hits = 10;


	ScoreBoard scoreBoard;

	// Use this for initialization
	void Start ()
	{
		AddBoxCollider();
		scoreBoard = FindObjectOfType<ScoreBoard>();
	}

	private void AddBoxCollider()
	{
		Collider boxCollider = gameObject.AddComponent<BoxCollider>();
		boxCollider.isTrigger = false;
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnParticleCollision(GameObject other)
	{
		if (other.tag != "Terrain")
		{
			ProcessHit();
		}

		if (hits <= 0) // <= accounts for overshoot
		{
			KillEnemy();
		}
	}

	private void ProcessHit()
	{
		scoreBoard.ScoreHit(scorePerHit);
		hits = hits - 1;
		// todo - consider hit fx
	}

	private void KillEnemy()
	{
		GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
		fx.transform.parent = parent;
		Destroy(gameObject);
	}
}
