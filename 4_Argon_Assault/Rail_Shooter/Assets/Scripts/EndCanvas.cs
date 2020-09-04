using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndCanvas : MonoBehaviour {

	[SerializeField] GameObject titleText;
	[SerializeField] GameObject scoreText;
	[SerializeField] GameObject againButton;
	public RectTransform scoreInt;

	public bool successful = false;
	public bool isEnd = false;

	void Awake()
	{
		int numCanvas = FindObjectsOfType<EndCanvas>().Length;

		if (numCanvas > 1)
		{
			Destroy(this.gameObject);
		}
		else
		{
			DontDestroyOnLoad(this.gameObject);
		}
	}

	public void DisplayEndCanvas (bool isDisplayed)
    {
		if (successful)
		{
			titleText.GetComponent<Text>().text = "Mission Success";
		}
		else
		{
			titleText.GetComponent<Text>().text = "Game Over";
		}

		if (isDisplayed)
		{
			scoreInt.anchoredPosition = new Vector3(1209f, 519.7f, 0f);
		}
		else
		{
			scoreInt.anchoredPosition = new Vector3(277f, 134f, 0f);
		}		

		titleText.SetActive(isDisplayed);
		scoreText.SetActive(isDisplayed);
		againButton.SetActive(isDisplayed);
		scoreInt.gameObject.SetActive(isDisplayed);
    }

	public void PlayAgainButton()
	{
		SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();
		ScoreBoard scoreBoard = scoreInt.gameObject.GetComponent<ScoreBoard>();
		scoreBoard.ResetScore();
		DisplayEndCanvas(false);
		sceneLoader.LoadFirstScene();
		Destroy(this.gameObject);
	}
}
