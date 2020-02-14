using UnityEngine;
using UnityEngine.SceneManagement;


public class Rocket : MonoBehaviour {

	Rigidbody rigidBody;
	AudioSource audioSource;

	bool isTransitioning = false;

	bool collisionsDisabled = false;

	[SerializeField] float rcsThrust = 250f;
	[SerializeField] float mainThrust = 25f;
	[SerializeField] float loadLevelDelay = 2f;

	[SerializeField] AudioClip mainEngine;
	[SerializeField] AudioClip death;
	[SerializeField] AudioClip success;

	[SerializeField] ParticleSystem mainEngineParticles;
	[SerializeField] ParticleSystem deathParticles;
	[SerializeField] ParticleSystem successParticles;

	// Use this for initialization
	void Start () 
	{
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}

	
	// Update is called once per frame
	void Update () 
	{
		if (!isTransitioning)
		{
			RespondToThrustInput();
			RespondToRotateInput();
		}

		// debug keys available whenever
		if (Debug.isDebugBuild)
		{
			RespondToDebugKeys();
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (isTransitioning || collisionsDisabled) { return;	}

		switch (collision.gameObject.tag)
		{
			case "Friendly":
				break;
			case "Finish":
				StartSuccessSequence();
				break;
			default:
				StartDeathSequence();
				break;
		}
	}


	private void StartSuccessSequence()
	{
		isTransitioning = true;
		audioSource.Stop();
		audioSource.PlayOneShot(success);
		successParticles.Play();
		Invoke("LoadNextLevel", loadLevelDelay);
	}


	private void StartDeathSequence()
	{
		isTransitioning = true;
		audioSource.Stop();
		audioSource.PlayOneShot(death);
		deathParticles.Play();
		Invoke("LoadFirstLevel", loadLevelDelay);
	}


	private void LoadNextLevel()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		int nextSceneIndex = currentSceneIndex + 1;

		if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
		{
			LoadFirstLevel();
		}
		else
		{
			SceneManager.LoadScene(nextSceneIndex);
		}


		/*
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
		}*/
	}


	private void LoadFirstLevel()
	{
		SceneManager.LoadScene(0);
	}


	private void RespondToThrustInput()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			ApplyThrust();
		}
		else
		{
			audioSource.Stop();
			mainEngineParticles.Stop();
		}
	}

	private void ApplyThrust()
	{
		// print("Thrusting");
		rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
		if (!audioSource.isPlaying)
		{
			audioSource.PlayOneShot(mainEngine);
		}
		mainEngineParticles.Play();
	}

	private void RespondToDebugKeys()
	{
		if (Input.GetKeyDown(KeyCode.L))
		{
			LoadNextLevel();
		}
		else if (Input.GetKeyDown(KeyCode.C))
		{
			// toggle collision
			collisionsDisabled = !collisionsDisabled;
		}
	}

	private void RespondToRotateInput()
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
