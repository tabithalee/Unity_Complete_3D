using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour {

	[SerializeField] float levelLoadTime;

	[SerializeField] Animation anim;

	void Awake()
    {
		AudioSource myAudioSource = GetComponent<AudioSource>();
		myAudioSource.time = 1f;
		myAudioSource.Play();
    }

	// Use this for initialization
	void Start () {
		anim["cackle"].speed = 0.5f;
		anim.Play("cackle");
		StartCoroutine(LoadLevelAgain());
	}
	
	IEnumerator LoadLevelAgain()
    {
		yield return new WaitForSeconds(levelLoadTime);
		SceneManager.LoadScene("5 Tower Defense");
    }
	
}
