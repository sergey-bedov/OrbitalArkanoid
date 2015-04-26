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
		[SerializeField]
		private int curLevel;
		
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
			if( levelControl == null )
				levelControl = this;
			else
				GameObject.Destroy( this.gameObject );
			
			levelsGOs = new GameObject[0];
			foreach(GameObject levelGO in Resources.LoadAll("Prefabs/Levels", typeof(GameObject)))
			{
				levelsGOs = ArrayTools.PushLast(levelsGOs, levelGO);
			}
		//	curLevel = 0;
		//	SetLevel(3);
		}
		#endregion

		// TODO Make PAUSE work as a boolean in GameController
		public void StartLevel (int num)
		{
			if (Application.loadedLevelName != "GameLevel")
				Application.LoadLevel("GameLevel");

			StartCoroutine(StartLevelCountdown(num));
		}
		IEnumerator StartLevelCountdown(int num)
		{
			yield return new WaitForSeconds(3);
			SetLevel(num);
		}
		public void StartLevel ()
		{
			StartLevel(curLevel);
		}

		public void SetLevel (int levelNumber)
		{
			if (levelNumber <= levelsGOs.Length)
			{
				Level l = FindObjectOfType(typeof(Level)) as Level;
				if (l) Destroy(l.gameObject);
				print ("LevelsQty: " + levelsGOs.Length + "; ChousenLevel: " + levelNumber);
				Instantiate(levelsGOs[levelNumber]);
				curLevel = levelNumber;
				GuiController.Get().UpdateLevelInfo(levelsGOs[curLevel].GetComponent<Level>());
			}
			else
			{
				Debug.LogError("There is no Level #" + levelNumber + "!!! Max Number of Levels are " + levelsGOs.Length + ".");
			}
		}

		public void NextLevel ()
		{
			StartLevel(curLevel + 1);
		}

		public Level GetLevel(int num)
		{
			return levelsGOs[num].GetComponent<Level>();
		}
	}
}
