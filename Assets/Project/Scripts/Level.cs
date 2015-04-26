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
		BlocksTotal = GetComponentsInChildren<Cell>().Length;
		UpdateBlocksLeft();
	}
	public void UpdateBlocksLeft()
	{
		BlocksLeft = GetComponentsInChildren<Cell>().Length;
		if (BlocksLeft == 0)
			LevelCompleted();
	}
	private void LevelCompleted()
	{
		LevelController.Get().NextLevel();
		Debug.Log("Level Completed.");
	}
}
