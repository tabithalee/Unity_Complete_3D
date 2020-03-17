﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ok as long as this is the only script that loads scenes

public class CollisionHandler : MonoBehaviour {

	[Tooltip("FX prefab on player")][SerializeField] GameObject deathFX;

	[Tooltip("In seconds")][SerializeField] float levelLoadDelay = 1f;


	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Landing Pad")
		{
			StartDeathSequence();
			deathFX.SetActive(true);
			Invoke("ReloadScene", levelLoadDelay);
		}
	}

	private void StartDeathSequence()
	{
		SendMessage("OnPlayerDeath");
	}

	private void ReloadScene() // string referenced
	{
		SceneManager.LoadScene(1);
	}
}
