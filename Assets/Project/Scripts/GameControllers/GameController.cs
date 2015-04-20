using UnityEngine;
using System.Collections;

//Sergey Bedov - 4/18/2015

namespace SB.Controllers
{
	public class GameController : MonoBehaviour 
	{
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
			if( gameControl == null )
				gameControl = this;
			else
				GameObject.Destroy( this.gameObject );

			// If we load level with Board & Ball:
			boardController = BoardController.Get();
			ballController = BallController.Get();
			levelController = LevelController.Get ();
			guiController = GuiController.Get ();
		}
		#endregion

		void Update ()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Pause();
			}
		}

		float timeScale = 1;
		public void Pause ()
		{
			// --- Deal with Menu Spawn ---


			// --- Deal with Time Stop ---
			if (Time.timeScale > 0)
			{
				timeScale = Time.timeScale;
				Time.timeScale = 0;
			}
			else
			{
				Time.timeScale = timeScale;
			}
			GuiController.Get().ShowHidePauseMenu();
		}

		public void LostBalls()
		{

		}

		public void TempSetLevel(int level)
		{
			LevelController.Get().SetLevel(level);
		}

		public void ExitGame()
		{
			Debug.Log("ExitGame");
			Application.Quit();
		}

	}

}
