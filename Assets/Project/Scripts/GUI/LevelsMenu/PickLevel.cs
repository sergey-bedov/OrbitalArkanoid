using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SB.Controllers;

public class PickLevel : MonoBehaviour
{
	public Button[] LevelsButtons;

	void Awake()
	{
		int topLevel = GameController.Get().TheGameVariables.TopLevel;;
		for (int i = 0; i < LevelsButtons.Length; i++)
		{
			if (i <= topLevel)
				LevelsButtons[i].interactable = true;
			else
				LevelsButtons[i].interactable = false;
		}
		ChouseLevel(topLevel);
	}

	public void ChouseLevel(int num)
	{
		FindObjectOfType<LevelPanel>().PapulatePanel(LevelController.Get().GetLevel(num));
		print ("PickLevel | ChouseLevel(" + num + "))");
	}
}
