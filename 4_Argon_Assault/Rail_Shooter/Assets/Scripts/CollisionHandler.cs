using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ok as long as this is the only script that loads scenes

public class CollisionHandler : MonoBehaviour {

	[Tooltip("FX prefab on player")][SerializeField] GameObject deathFX;

	[Tooltip("In seconds")] public float levelLoadDelay = 1f;

	[SerializeField] EndCanvas endCanvas;
	[SerializeField] BoxCollider boxCollider;

	void Start ()
	{
		StartCoroutine(EnableLandingPad());
	}

	IEnumerator EnableLandingPad()
	{
		BoxCollider myBoxCollider = GetComponent<BoxCollider>();
		yield return new WaitForSeconds(5f);
		myBoxCollider.isTrigger = true;
		boxCollider.isTrigger = true;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Landing Pad")
		{
			StartDeathSequence();
			deathFX.SetActive(true);
			endCanvas.successful = false;
		}
		else 
		{ 
			endCanvas.successful = true;
		}

		endCanvas.isEnd = true;
		Invoke("ReloadScene", levelLoadDelay);
	}

	private void StartDeathSequence()
	{
		SendMessage("OnPlayerDeath");
	}

	private void ReloadScene() // string referenced
	{
		SceneManager.LoadScene("End");
	}
}
