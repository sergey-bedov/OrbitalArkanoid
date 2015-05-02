using UnityEngine;
using System.Collections;

//Sergey Bedov - 4/18/2015

namespace SB.Controllers
{
	public class GameController : MonoBehaviour 
	{
		public GameVariables TheGameVariables;

		private MusicController musicController;
		private SoundController soundController;

		private BoardController boardController;
		private BallController ballController;
		private LevelController levelController;
		private GuiController guiController;

		public bool IsOnPause; // TODO

		#region Access Instance Anywhere
		private static GameController gameControl;
		public static GameController Get()
		{
			if( gameControl != null )
				return gameControl;
			else
			{
				GameObject obj = new GameObject("GameController");
				obj.tag = "Controller";
				gameControl = obj.AddComponent<GameController>();
				return gameControl;
			}
		}
		void Awake() 
		{
			Debug.Log(this.GetType());
			DontDestroyOnLoad(transform.gameObject);
			if( gameControl == null )
				gameControl = this;
			else
				GameObject.Destroy( this.gameObject );

			TheGameVariables = gameObject.AddComponent<GameVariables>();

			musicController = MusicController.Get ();
			soundController = SoundController.Get ();

			//IsOnPause = true;
			// If we load level with Board & Ball:
			boardController = BoardController.Get();
		//	ballController = BallController.Get();
			levelController = LevelController.Get ();
			guiController = GuiController.Get ();
			TempSetControlType(0);
		}
		#endregion

		void Update ()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				PauseTrigger();
			}
		}

		// float timeScale = 1;
		public void PauseGame ()
		{
			BoardController.Get().Pause();
			BallController.Get().Pause();
		}
		public void UnPauseGame ()
		{
			BoardController.Get().UnPause();
			BallController.Get().UnPause();
		}
		public void PauseTrigger ()
		{
			IsOnPause = !IsOnPause;
			if (IsOnPause)
			{
				UnPauseGame();
			}
			else
			{
				PauseGame();
			}
		}

		public void LostBalls()
		{

		}

		public void TempSetLevel(int level)
		{
			LevelController.Get().SetLevel(level);
		}

		private GameObject arrowsPanel;
		public void TempSetControlType(int controlType)
		{
			print (controlType);
			if (controlType == 0)
			{
				if (arrowsPanel == null) arrowsPanel = GameObject.Find("ControlsArrowsPanel");
				if (arrowsPanel != null)
					arrowsPanel.SetActive(true);
			}
			else
			{
				if (GameObject.Find("ControlsArrowsPanel") != null)
					arrowsPanel = GameObject.Find("ControlsArrowsPanel");
				arrowsPanel.SetActive(false);
			}
			BoardController.Get().ControlType = controlType;
			print (arrowsPanel.name);
		}

		public void BoardConstantMove(float speed)
		{
			BoardController.Get().ConstantMove(speed);
		}

		public void BackToLevelsMenu()
		{
			Cleanup();
			Application.LoadLevel("LevelsMenu");
		}
		public void BackToMainMenu()
		{
			Cleanup();
			Application.LoadLevel("MainMenu");
		}
		public void Cleanup()
		{
			BoardController.Get().Cleanup();
			BallController.Get().Cleanup();
		}
		public void ExitGame()
		{
			Debug.Log("ExitGame");
			Application.Quit();
		}

	}

}
