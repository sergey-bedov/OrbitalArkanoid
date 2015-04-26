using UnityEngine;
using System.Collections;

namespace SB.Controllers
{
	public class GuiController : MonoBehaviour
	{
		private GameObject canvasBack;
		private GameObject canvasFront;

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

			InstantiateCanvas();

			levelInfoPanel = FindObjectOfType(typeof(LevelInfoPanel)) as LevelInfoPanel;
		}
		#endregion

		public void UpdateLevelInfo(Level level)
		{
			levelInfoPanel.LevelNumber.text = level.Number.ToString();
			levelInfoPanel.LevelName.text = level.Name;
		//	levelInfoPanel.Score.text = level.Score.ToString();
		//	levelInfoPanel.Lives.text = level.Lives.ToString();
		}

		private void InstantiateCanvas()
		{
			canvasBack = GameObject.Find("CanvasBack");
			if (canvasBack == null)
			{
				canvasBack = Instantiate (Resources.Load("Prefabs/GUI/CanvasBack", typeof(GameObject))) as GameObject;
			}
			canvasFront = GameObject.Find("CanvasFront");
			if (canvasFront == null)
			{
				canvasFront = Instantiate (Resources.Load("Prefabs/GUI/CanvasFront", typeof(GameObject))) as GameObject;
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
			InstantiateCanvas();
			PauseMenu.transform.SetParent(canvasFront.transform, false);
		}
		private void HidePauseMenu ()
		{
			GameController.Get().UnPauseGame();
			Destroy (PauseMenu);
		}
		#endregion
	}
}
