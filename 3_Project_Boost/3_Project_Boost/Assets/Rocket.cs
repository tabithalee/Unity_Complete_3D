using UnityEngine;
using UnityEngine.SceneManagement;

//TODO - fix lighting bug

public class Rocket : MonoBehaviour {

	Rigidbody rigidBody;
	AudioSource audioSource;

	[SerializeField] float rcsThrust = 250f;
	[SerializeField] float mainThrust = 25f;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}

	
	// Update is called once per frame
	void Update () {
		Thrust();
		Rotate();
	}

	void OnCollisionEnter(Collision collision)
	{
		switch (collision.gameObject.tag)
		{
			case "Friendly":
				break;
			case "Finish":
				print("Hit Finish");
				switch (SceneManager.GetActiveScene().buildIndex)
				{
					case 0:
						SceneManager.LoadScene(1);
						break;
					case 1:
						SceneManager.LoadScene(2);
						break;
					case 2:
						SceneManager.LoadScene(2);
						break;
				}				
				break;
			default:
				print("dead");
				SceneManager.LoadScene(0);
				break;
		}
	}


	private void Thrust()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			// print("Thrusting");
			rigidBody.AddRelativeForce(Vector3.up * mainThrust);
			if (!audioSource.isPlaying)
			{
				audioSource.Play();
			}
		}
		else
		{
			audioSource.Stop();
		}
	}


	private void Rotate()
	{
		rigidBody.freezeRotation = true; // take manual control of rotation

		float rotationThisFrame = rcsThrust * Time.deltaTime;

		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(Vector3.forward * rotationThisFrame);
		}
		else if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(-Vector3.forward * rotationThisFrame);
		}

		rigidBody.freezeRotation = false; // resume physics control of rotation
	}
}
