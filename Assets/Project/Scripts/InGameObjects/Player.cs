using UnityEngine;
using System.Collections;

namespace SB
{
	public class Player : MonoBehaviour
	{
		public string Name;
		public int Level;

		public int Lives = 5;
		public int Score = 0;

		void Awake()
		{
			Name = PlayerPrefs.GetString("Name", "NoName");
			Level = PlayerPrefs.GetInt("Level", 0);
		}

		void OnDestroy()
		{
			PlayerPrefs.SetString("Name", Name);
			PlayerPrefs.SetInt("Level", Level);
		}
	}
}
