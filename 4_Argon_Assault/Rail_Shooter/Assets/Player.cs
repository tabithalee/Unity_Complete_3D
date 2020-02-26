using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

	[Tooltip("In ms^-1")][SerializeField] float xySpeed = 20f;
	[Tooltip("In m")][SerializeField] float xRange = 9f;
	[Tooltip("In m")] [SerializeField] float yRange = 5f;

	[SerializeField] float positionPitchFactor = -5f;
	[SerializeField] float controlPitchFactor = -30f;
	[SerializeField] float positionYawFactor = 4f;
	[SerializeField] float controlRollFactor = -20f;


	float xThrow, yThrow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		ProcessTranslation();
		ProcessRotation();
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

		float xOffset = xThrow * xySpeed * Time.deltaTime;
		float rawXPos = transform.localPosition.x + xOffset;

		float yOffset = yThrow * xySpeed * Time.deltaTime;
		float rawYPos = transform.localPosition.y + yOffset;

		float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
		float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

		transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
	}
}
