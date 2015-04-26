using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SB.Controllers;

public class LevelPanel : MonoBehaviour
{
	public Image LevelImage;
	public LevelInfoPanel levelInfoPanel;
	public Button StartLevelButton;

	public void PapulatePanel(Level level)
	{
		LevelImage.sprite = level.LevelImage;
		levelInfoPanel.PapulatePanel(level);
	}

	public void StartTheLevel()
	{
		int levelNum = int.Parse(levelInfoPanel.LevelNumber.text);
		LevelController.Get().StartLevel(levelNum);

	}
}
