using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SB.Controllers;

public class Countdown : MonoBehaviour
{
	public Text LevelNameText;
	public Text CountdownText;

	void Awake ()
	{
		GameController.Get().PauseGame();
	}

	public void SetLevelNumName(string name)
	{
		LevelNameText.text = name;
	}
	public void SetLevelNumName(Level level)
	{
		if (level != null)
			LevelNameText.text = "#" + level.Number + ". " + level.Name;
	}
	public void SetCountdown(string countText)
	{
		CountdownText.text = countText;
	}

	public void KillCountdown ()
	{
		GameController.Get().UnPauseGame();
		Destroy (gameObject);
	}
}
