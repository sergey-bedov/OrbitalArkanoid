using UnityEngine;
using System.Collections;
using SB.InGameObjects;

namespace SB.Controllers
{
	using SB.InGameObjects.Cells;
	public class LevelController : MonoBehaviour
	{
		[SerializeField]
		private GameObject[] levelsGOs;
		public int CurLevelNum;
		
		#region Access Instance Anywhere
		private static LevelController levelControl;
		public static LevelController Get()
		{
			if( levelControl != null )
				return levelControl;
			else
			{
				GameObject obj = new GameObject("LevelController");
				obj.transform.SetParent(GameController.Get().transform);
				obj.tag = "Controller";
				levelControl = obj.AddComponent<LevelController>();
				return levelControl;
			}
		}
		void Awake() 
		{
			DontDestroyOnLoad(transform.gameObject);
			if( levelControl == null )
				levelControl = this;
			else
				GameObject.Destroy( this.gameObject );
			
			levelsGOs = new GameObject[0];
			foreach(GameObject levelGO in Resources.LoadAll("Prefabs/Levels", typeof(GameObject)))
			{
				if (levelGO.GetComponent<Level>().Number < 100)
					levelsGOs = ArrayTools.PushLast(levelsGOs, levelGO);
			}
		}
		#endregion

		public void StartLevel (int num)
		{
			CurLevelNum = num;
			if (num < levelsGOs.Length)
			{
				StartCoroutine(StartLevelCountdown());
			}
			else //if (num == levelsGOs.Length)
			{
				GameController.Get().TheEnd();
			}

		}
		IEnumerator StartLevelCountdown()
		{
		//	Application.LoadLevel("GameLevel");
			while (Application.loadedLevelName != "GameLevel")
				yield return new WaitForEndOfFrame();
			print ("Start Level Num " + CurLevelNum);
			SetLevel (CurLevelNum);
		}

		public void StartLevel ()
		{
			StartLevel(CurLevelNum);
		}

		public void SetLevel (int levelNumber)
		{
			if (levelNumber < levelsGOs.Length)
			{
				Level l = FindObjectOfType(typeof(Level)) as Level;
				if (l) Destroy(l.gameObject);
				print ("LevelsQty: " + levelsGOs.Length + "; ChousenLevel: " + levelNumber);
				Instantiate(levelsGOs[levelNumber]);
				GameObject.Find("PanelCenter").GetComponent<UnityEngine.UI.Image>().sprite = levelsGOs[levelNumber].GetComponent<Level>().BackgroundImage;
				// CurLevelNum = levelNumber;
				GuiController.Get().UpdateLevelInfo(levelsGOs[levelNumber].GetComponent<Level>());
			}
			else
			{
				Debug.LogError("There is no Level #" + levelNumber + "!!! Max Number of Levels are " + levelsGOs.Length + ".");
			}
		}

		public void NextLevel ()
		{
			StartLevel(CurLevelNum + 1);
		}


		public Level GetLevel(int num)
		{
			if (num < levelsGOs.Length)
				return levelsGOs[num].GetComponent<Level>();
			else
			{
				return null;
			}
		}
		public Level GetCurLevel()
		{
			return GetLevel(CurLevelNum);
		}
	}
}
