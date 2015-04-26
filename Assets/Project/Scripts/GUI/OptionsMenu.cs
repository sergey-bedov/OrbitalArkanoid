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
		#endregion

		private GameVariables gv;

		void Awake()
		{
			Debug.Log(this.GetType());
			gv = GameController.Get().TheGameVariables;
			LoadVariables();
		}

		void Update()
		{
			UpdateVariables();
		}

		void LoadVariables()
		{
			Mute.isOn = gv.Mute;
			Sound.value = gv.SoundEffects;
			Music.value = gv.Music;
		}
		void UpdateVariables()
		{
			gv.Mute = Mute.isOn;
			gv.SoundEffects = Sound.value;
			gv.Music = Music.value;
		}
		void SaveVariables()
		{
			UpdateVariables();
			gv.SaveOptions();
		}

		void OnDestroy()
		{
			gv.Mute = Mute.isOn;
			SaveVariables();
		}
	}
}