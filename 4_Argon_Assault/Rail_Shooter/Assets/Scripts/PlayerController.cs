﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

	[Header("General")]
	[Tooltip("In ms^-1")][SerializeField] float controlSpeed = 30f;
	[Tooltip("In m")][SerializeField] float xRange = 9f;
	[Tooltip("In m")] [SerializeField] float yRange = 5f;

	[Header("Screen-Position Based")]
	[SerializeField] float positionPitchFactor = -5f;
	[SerializeField] float positionYawFactor = 4f;

	[Header("Control-throw Based")]
	[SerializeField] float controlPitchFactor = -30f;
	[SerializeField] float controlRollFactor = -20f;
	[SerializeField] GameObject[] guns;

	float xThrow, yThrow;

	bool isControlEnabled = true;

	// Update is called once per frame
	void Update ()
	{
		if (isControlEnabled)
		{
			ProcessTranslation();
			ProcessRotation();
			ProcessFiring();
		}
	}


	private void ProcessRotation()
	{
		float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
		float pitchDueToControlThrow = yThrow * controlPitchFactor;
		float pitch = pitchDueToPosition + pitchDueToControlThrow;
		
		float yaw = transform.localPosition.x * positionYawFactor;
		
		float roll = xThrow * controlRollFactor;
		
		transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
	}

	private void ProcessTranslation()
	{
		xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
		yThrow = CrossPlatformInputManager.GetAxis("Vertical");

		float xOffset = xThrow * controlSpeed * Time.deltaTime;
		float rawXPos = transform.localPosition.x + xOffset;

		float yOffset = yThrow * controlSpeed * Time.deltaTime;
		float rawYPos = transform.localPosition.y + yOffset;

		float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
		float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

		transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
	}


	private void ProcessFiring()
	{
		if (CrossPlatformInputManager.GetButton("Fire"))
		{
			SetGunsActive(true);
		}
		else
		{
			SetGunsActive(false);
		}
	}


	private void SetGunsActive(bool isActive)
	{
		foreach (GameObject gun in guns) // care may affect death FX
		{
			var emissionModule = gun.GetComponent<ParticleSystem>().emission;
			emissionModule.enabled = isActive;
		}
	}


	void OnPlayerDeath() // called by string reference
	{
		isControlEnabled = false;
	}
}
