using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

	[SerializeField] int health = 10;
	[SerializeField] int healthDecrease = 1;
	[SerializeField] Text healthText;
	[SerializeField] AudioClip playerDamageSFX;

	float loadLevelDelay = 0.5f;


	void Start ()
	{
		healthText.text = "Lives: " + health.ToString();
	}

	void OnTriggerEnter(Collider other)
	{
		GetComponent<AudioSource>().PlayOneShot(playerDamageSFX);
		health -= healthDecrease;
		healthText.text = "Lives: " + health.ToString();

		if (health == 0)
		{
			StartCoroutine(LoadCackleScreen());
		}
	}

	IEnumerator LoadCackleScreen()
	{
		yield return new WaitForSeconds(loadLevelDelay);
		SceneManager.LoadScene("Dead Screen");
	}
}
