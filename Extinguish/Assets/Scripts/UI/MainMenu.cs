using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public bool fadeOut = false;
	public bool fadeIn = false;
	public Image uiImage;

	void Start()
	{
		fadeIn = false;
		fadeOut = true;
	}

	void Update()
	{
		FadeInOut();
	}

	public void OnClick_Start()
	{
		SceneManager.LoadScene(Scenes.LevelOne);
	}

	public void OnClick_Exit()
	{
		Application.Quit();
	}

	public void OnClick_Retry()
	{
		fadeOut = true;
		SceneManager.LoadScene(PlayerPrefs.GetString("CurrentLevel"));
	}


	void FadeInOut()
	{
		if (fadeOut)
		{
			uiImage.canvasRenderer.SetAlpha(1f);
			uiImage.CrossFadeAlpha(0f, 1, false);
			fadeOut = false;
			fadeIn = true;
		}
	}
}
