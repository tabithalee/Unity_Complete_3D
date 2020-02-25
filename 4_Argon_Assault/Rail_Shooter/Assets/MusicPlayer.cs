using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	// Use this for initialization
	void Start () {
		Invoke("LoadFirstScene", 10.0f);
	}

	void LoadFirstScene()
	{
		SceneManager.LoadScene(1);
	}
}
