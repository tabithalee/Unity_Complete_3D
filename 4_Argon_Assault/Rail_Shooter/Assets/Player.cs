using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

	[Tooltip("In ms^-1")][SerializeField] float xySpeed = 20f;
	[SerializeField] float xRangeMax = 5f;
	[SerializeField] float xRangeMin = -9.4f;
	[SerializeField] float yRangeMax = 3f;
	[SerializeField] float yRangeMin = -3.8f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
		float yThrow = CrossPlatformInputManager.GetAxis("Vertical");

		float xOffset = xThrow * xySpeed * Time.deltaTime;
		float rawXPos = transform.localPosition.x + xOffset;

		float yOffset = yThrow * xySpeed * Time.deltaTime;
		float rawYPos = transform.localPosition.y + yOffset;

		float clampedXPos = Mathf.Clamp(rawXPos, xRangeMin, xRangeMax);
		float clampedYPos = Mathf.Clamp(rawYPos, yRangeMin, yRangeMax);

		transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
	}
}
