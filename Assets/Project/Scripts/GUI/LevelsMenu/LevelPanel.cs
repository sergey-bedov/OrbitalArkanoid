using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SB.Controllers;

public class LevelPanel : MonoBehaviour
{
	public Image LevelImage;
	public LevelInfoPanel levelInfoPanel;
	public Button StartLevelButton;

	void Awake ()
	{
		PapulatePanel(LevelController.Get().GetLevel(GameVariables.TopLevel));
	}

	public void PapulatePanel(Level level)
	{
		LevelImage.sprite = level.LevelImage;
		levelInfoPanel.PapulatePanel(level);
	}

	public void StartTheLevel()
	{
		int levelNum = int.Parse(levelInfoPanel.LevelNumber.text);
		Debug.Log("Load Scene: GameLevel;");
		Application.LoadLevel("GameLevel");
		LevelController.Get().StartLevel(levelNum);

	}
}
