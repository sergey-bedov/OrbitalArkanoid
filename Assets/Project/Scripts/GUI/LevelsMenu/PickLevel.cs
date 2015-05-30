using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SB.Controllers;

public class PickLevel : MonoBehaviour
{
	public Button[] LevelsButtons;

	void Awake()
	{

		if (Application.loadedLevelName != "GameLevel")
			for (int i = 0; i < LevelsButtons.Length; i++)
			{
				if (i <= GameVariables.TopLevel)
					LevelsButtons[i].interactable = true;
				else
					LevelsButtons[i].interactable = false;
			}
		ChouseLevel(GameVariables.TopLevel);
	}

	float doubleClickStart = -1F;
	public void ChouseLevel(int num)
	{
		if ((Time.time - doubleClickStart) < 0.3f)
		{
			Debug.Log("Double Clicked!");
			this.LaunchLevel(num);
			doubleClickStart = -1;
		}
		else
		{
			Debug.Log("Single Clicked!");
			PapulateLevelPanel(num);
			PapulateHumanPanel(num);
			doubleClickStart = Time.time;
		}
	}
	public void PapulateLevelPanel(int num)
	{
		if (FindObjectOfType<LevelPanel>() != null)
			FindObjectOfType<LevelPanel>().PapulatePanel(LevelController.Get().GetLevel(num));
	//	print ("PickLevel | ChouseLevel(" + num + "))");
	}
	public void PapulateHumanPanel(int num)
	{
		if (FindObjectOfType<HumanPanel>() != null)
			FindObjectOfType<HumanPanel>().PapulatePanel(num);
	}
	public void LaunchLevel (int num)
	{
		Application.LoadLevel("GameLevel");
		GameVariables.CurrentLevel = num;
		LevelController.Get().StartLevel(num);
	}
}
