using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SB.Controllers;

public class LevelsMenu : MonoBehaviour
{
	public Button ButContinue;
	public Button ButRandomLevel;
	
	public void Continue()
	{
		if (FindObjectOfType<PickLevel>() != null)
			FindObjectOfType<PickLevel>().ChouseLevel(GameVariables.TopLevel);
		if (FindObjectOfType<LevelPanel>() != null)
			FindObjectOfType<LevelPanel>().StartTheLevel();
	}
	public void RandomLevel()
	{
		
	}
}
