using UnityEngine;
using System.Collections;

namespace SB.Controllers
{
	public class GuiController : MonoBehaviour
	{
		public LevelInfoPanel levelInfoPanel;

		public GameObject PauseMenu;

		#region Access Instance Anywhere
		private static GuiController guiControl;
		public static GuiController Get()
		{
			if( guiControl != null )
				return guiControl;
			else
			{
				GameObject obj = new GameObject("GuiController");
				obj.transform.SetParent(GameController.Get().transform);
				obj.tag = "Controller";
				guiControl = obj.AddComponent<GuiController>();
				return guiControl;
			}
		}
		void Awake() 
		{
			DontDestroyOnLoad(transform.gameObject);
			if( guiControl == null )
				guiControl = this;
			else
				GameObject.Destroy( this.gameObject );


		}
		#endregion

		public void UpdateLevelInfo(Level level)
		{
			if (levelInfoPanel == null)
				levelInfoPanel = FindObjectOfType(typeof(LevelInfoPanel)) as LevelInfoPanel;
			levelInfoPanel.LevelNumber.text = level.Number.ToString();
			if (levelInfoPanel.LevelName)
				levelInfoPanel.LevelName.text = level.Name;
		//	levelInfoPanel.Score.text = level.Score.ToString();
		//	levelInfoPanel.Lives.text = level.Lives.ToString();
		}

		public Countdown BornCountdown ()
		{
			GameController.Get().PauseGame();
			if (FindObjectOfType(typeof(Countdown)))
			{
				return (Countdown)FindObjectOfType(typeof(Countdown));
			}
			else
			{
				GameObject countdown = Instantiate (Resources.Load("Prefabs/GUI/Countdown", typeof(GameObject))) as GameObject;
				countdown.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
				return countdown.GetComponent<Countdown>();
			}
		}

		#region PauseMenu
		public void PauseMenuTrigger()
		{
			if (PauseMenu)
				HidePauseMenu();
			else
				ShowPauseMenu();
		}
		private void ShowPauseMenu ()
		{
			GameController.Get().PauseGame();
			PauseMenu = Instantiate (Resources.Load("Prefabs/GUI/PauseMenu", typeof(GameObject))) as GameObject;
			PauseMenu.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
		}
		private void HidePauseMenu ()
		{
			GameController.Get().UnPauseGame();
			Destroy (PauseMenu);
		}
		#endregion
	}
}
