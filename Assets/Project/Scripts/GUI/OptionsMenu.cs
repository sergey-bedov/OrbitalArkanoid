using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace SB.Controllers
{
	public class OptionsMenu : MonoBehaviour
	{
		#region Sound & Music GUI
		public Toggle Mute;
		public Slider Sound;
		public Slider Music;
		public Text TopLevelText;
		#endregion

	//	private GameVariables gv;

		void Awake()
		{
			Debug.Log(this.GetType());
		//	gv = GameController.Get().TheGameVariables;
			LoadVariables();
		}

		void Update()
		{
			UpdateVariables();
		}

		void LoadVariables()
		{
			Mute.isOn = GameVariables.Mute;
			Sound.value = GameVariables.SoundEffects;
			Music.value = GameVariables.Music;
			TopLevelText.text = ""+GameVariables.TopLevel;
		}
		void UpdateVariables()
		{
			GameVariables.Mute = Mute.isOn;
			GameVariables.SoundEffects = Sound.value;
			GameVariables.Music = Music.value;
		}
		public void ResetOptions()
		{
			GameVariables.ResetOptions();
			LoadVariables();
		}
		public void ResetProgress()
		{
			GameVariables.ResetProgress();
			LoadVariables();
		}
		void SaveVariables()
		{
			UpdateVariables();
			GameVariables.SaveOptions();
		}
	}
}