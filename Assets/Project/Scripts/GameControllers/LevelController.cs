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
			curLevel = 0;
			SetLevel(curLevel+1);
		}
		#endregion

		// TODO Make PAUSE work as a boolean in GameController
		public void StartLevel ()
		{
			//StartCoroutine(Countdown(3.0F)); 
		}
		/*
		IEnumerator Countdown (float waitTime)
		{
			Time.timeScale = 0;
			print (1);
			yield return new WaitForSeconds(waitTime);
			print ("GO !!!");
			Time.timeScale = 1;
		}
		*/

		public void SetLevel (int levelNumber)
		{
			curLevel = levelNumber-1;
			if (levelNumber <= levelsGOs.Length)
			{
				Level l = FindObjectOfType(typeof(Level)) as Level;
				if (l) Destroy(l.gameObject);
				Instantiate(levelsGOs[curLevel]);
			}
			else
			{
				Debug.LogError("There is no Level #" + levelNumber + "!!! Max Number of Levels are " + levelsGOs.Length + ".");
			}
			GuiController.Get().UpdateLevelInfo(levelsGOs[curLevel].GetComponent<Level>());
			StartLevel ();
		}

		public void NextLevel ()
		{
			SetLevel(curLevel + 1);
		}

	}
}
