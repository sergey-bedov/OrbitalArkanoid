using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	public Button ButPlay;
	public Button ButOptions;
	public Button ButAboutUs;
	public Button ButExit;

	void Awake()
	{
		SB.Controllers.GameController.Get();
	}

	public void Play()
	{
		Application.LoadLevel("LevelsMenu");
	}
	public void Options()
	{
		Application.LoadLevel("OptionsMenu");
	}
	public void AboutUs()
	{
		Application.LoadLevel("AboutUsMenu");
	}
	public void Exit()
	{
		Debug.Log("Exit Game.");
		Application.Quit();
	}
}
