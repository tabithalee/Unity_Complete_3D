using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	EndCanvas endCanvas;

	// Use this for initialization
	void Start()
	{
		endCanvas = FindObjectOfType<EndCanvas>();
		DisplayEnd();
	}

	public void LoadFirstScene()
	{
		SceneManager.LoadScene(1);
	}

	void DisplayEnd()
	{
		if (endCanvas.isEnd)
		{
			endCanvas.DisplayEndCanvas(true);
		}
	}
}
