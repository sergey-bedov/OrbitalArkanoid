using UnityEngine;
using System.Collections;
using SB.InGameObjects.Cells;
using SB.Controllers;

public class Level : MonoBehaviour
{
	public int Number;
	public string Name;
	public string Descripton;
	public Sprite BackgroundImage;

	public Sprite LevelImage; //To be shown in LevelsMenu

	public int BlocksTotal;
	public int BlocksLeft;

	// TODO move to Player
	public int Score;
	public int Lives;

	void Awake ()
	{
		// TODO rename Cell into Block
		if (Number != GameVariables.CurrentLevel) LevelController.Get().StartLevel(GameVariables.CurrentLevel);
		BlocksTotal = GetComponentsInChildren<Cell>().Length;
		StartCoroutine(StartLevelCountdown());
	}
	void Start ()
	{
		print ("Start level #" + Number + " '" + Name + "'" + "; Cur Scene: " + Application.loadedLevelName);
		if (Application.loadedLevelName == "GameLevel") UpdateBlocksLeft();
	}
	public void UpdateBlocksLeft()
	{
		BlocksLeft = GetComponentsInChildren<Cell>().Length;
		if (BlocksLeft == 0)
			LevelCompleted();
	}

	IEnumerator StartLevelCountdown()
	{
		print ("----- Started StartLevelCoroutine -----");
		
	//	yield return new WaitForSeconds(0.01F); // To Make Level appear
	//	LevelController.Get().SetLevel(Number);

		Countdown countdown;
		countdown = GuiController.Get().BornCountdown();
		countdown.SetLevelNumName(Name);
		countdown.SetCountdown ("3");
		yield return new WaitForSeconds(1F);
		countdown.SetCountdown ("2");
		yield return new WaitForSeconds(1F);
		countdown.SetCountdown ("1");
		yield return new WaitForSeconds(1F);
		countdown.SetCountdown ("GO");
		yield return new WaitForSeconds(1F);
		countdown.KillCountdown();
		print ("----- Finished StartLevelCoroutine -----");
	}

	private void LevelCompleted()
	{
		Debug.Log("Level Completed. " + BlocksLeft + "/" + BlocksTotal);
		if ((Number == GameVariables.TopLevel) && (Number != GameVariables.MaxLevel)) GameVariables.TopLevel++;
		GameVariables.CurrentLevel++;
		LevelController.Get().NextLevel();
	}
}
