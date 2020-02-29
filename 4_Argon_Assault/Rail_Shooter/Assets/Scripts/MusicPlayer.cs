using UnityEngine;

public class MusicPlayer : MonoBehaviour {

	void Awake()
	{
		int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
		print("music players in this scene: " + numMusicPlayers);
		// if more than one music player in scene
			// destroy ourselves
		// else
		DontDestroyOnLoad(this.gameObject);
	}
}
