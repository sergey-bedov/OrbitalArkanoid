using UnityEngine;
using System.Collections;

public static class GameVariables// : MonoBehaviour
{
	public static int CurrentLevel = 1;
	public static int MaxLevel = 10;
	#region Options
	// --- VOLUME ---
	public static bool Mute = false;
	public static float Music = 0.5F;
	public static float SoundEffects = 0.5F;
	// --- Volume Prefs Strings ---
	private static string prefMute = "Mute";
	private static string prefMusic = "Music";
	private static string prefSoundEffects = "SoundEffects";
	#endregion

	#region Progress Variables
	// --- GAME PROGRESS ---
	public static int TopLevel;
	// --- Volume Prefs Strings ---
	private static string prefTopLevel = "TopLevel";
	#endregion

	#region Progress Methods
	public static void ResetProgress()
	{
		TopLevel = 1;
		SaveProgress();
	}
	public static void SaveProgress()
	{
		PlayerPrefs.SetInt(prefTopLevel, TopLevel);
	}
	public static void LoadProgress()
	{
		TopLevel = PlayerPrefs.GetInt(prefTopLevel);
	}
	#endregion

	#region Options Methods
	public static void ResetOptions()
	{
	//	PlayerPrefs.DeleteAll();
		Mute = false;
		Music = 0.5F;
		SoundEffects = 0.5F;
		SaveOptions();
	}
	public static void SaveOptions()
	{
		int intMute;
		if (!Mute) intMute = 0; else intMute = 1;
		PlayerPrefs.SetInt(prefMute, intMute);
		PlayerPrefs.SetFloat(prefMusic, Music);
		PlayerPrefs.SetFloat(prefSoundEffects, SoundEffects);
		PlayerPrefs.Save();
	}
	public static void LoadOptions()
	{
		int intMute = PlayerPrefs.GetInt(prefMute, 0);
		if (intMute == 0) Mute=false; else Mute=true;
		Music = PlayerPrefs.GetFloat(prefMusic, 0.5F);
		SoundEffects = PlayerPrefs.GetFloat(prefSoundEffects, 0.5F);
	}
	#endregion

}