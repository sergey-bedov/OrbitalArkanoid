using UnityEngine;
using System.Collections;

namespace SB.Controllers
{
	public class GameVariables : MonoBehaviour
	{
		#region Access Instance Anywhere
//		private static GameVariables gameVariables;
//		public static GameVariables Get()
//		{
//			if( gameVariables != null )
//				return gameVariables;
//			else
//			{
//				GameObject obj = new GameObject("GameVariables");
//				obj.tag = "Controller";
//				obj.transform.SetParent(GameController.Get().transform);
//				gameVariables = obj.AddComponent<GameVariables>();
//				return gameVariables;
//			}
//		}
		void Awake() 
		{
			Debug.Log(Time.time + " | " + this.GetType());
//			DontDestroyOnLoad(transform.gameObject);
//			if( gameVariables == null )
//				gameVariables = this;
//			else
//				GameObject.Destroy( this.gameObject );

			LoadProgress();
			LoadOptions();
		}
		#endregion

		#region Options
		// --- VOLUME ---
		public bool Mute = true;
		public float Music = 0.5F;
		public float SoundEffects = 0.5F;
		// --- Volume Prefs Strings ---
		private string prefMute = "Mute";
		private string prefMusic = "Music";
		private string prefSoundEffects = "SoundEffects";
		#endregion

		#region Progress Variables
		// --- GAME PROGRESS ---
		public int TopLevel;
		// --- Volume Prefs Strings ---
		private string prefTopLevel = "TopLevel";
		#endregion

		#region Progress Methods
		public void ResetProgress()
		{
			TopLevel = 0;
		}
		private void SaveProgress()
		{
			PlayerPrefs.SetInt(prefTopLevel, TopLevel);
		}
		private void LoadProgress()
		{
			TopLevel = PlayerPrefs.GetInt(prefTopLevel);
		}
		#endregion

		#region Options Methods
		public void ResetOptions()
		{
			PlayerPrefs.DeleteAll();
			SaveOptions();
		}
		public void SaveOptions()
		{
			int intMute;
			if (!Mute) intMute = 0; else intMute = 1;
			PlayerPrefs.SetInt(prefMute, intMute);
			PlayerPrefs.SetFloat(prefMusic, Music);
			PlayerPrefs.SetFloat(prefSoundEffects, SoundEffects);
			PlayerPrefs.Save();
		}
		private void LoadOptions()
		{
			int intMute = PlayerPrefs.GetInt(prefMute, 0);
			if (intMute == 0) Mute=false; else Mute=true;
			Music = PlayerPrefs.GetFloat(prefMusic, 0.5F);
			SoundEffects = PlayerPrefs.GetFloat(prefSoundEffects, 0.5F);
		}
		#endregion

		void OnDestroy ()
		{
			Debug.Log("GAME VARIABLES DELETED\nSaving Variables to PlayerPrefs.");
			SaveProgress();
			SaveOptions();
		}
	}
}