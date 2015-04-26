using UnityEngine;
using System.Collections;

namespace SB.Controllers
{
	[RequireComponent (typeof(AudioSource))]
	public class MusicController : MonoBehaviour
	{
		public AudioClip [] MusicArray;
		AudioSource audioSource;

		#region Access Instance Anywhere
		private static MusicController musicControl;
		public static MusicController Get()
		{
			if( musicControl != null )
				return musicControl;
			else
			{

			//	GameObject obj = new GameObject("MusicController");
				GameObject obj = (GameObject)Instantiate(Resources.Load("Prefabs/Sound&Music/MusicController"));
				obj.name = "MusicController";
				obj.transform.SetParent(GameController.Get().transform);
				obj.tag = "Controller";
				musicControl = obj.GetComponent<MusicController>();
				return musicControl;
			}
		}
		void Awake() 
		{
			Debug.Log(this.GetType());
			DontDestroyOnLoad(transform.gameObject);
			if( musicControl == null )
				musicControl = this;
			else
				GameObject.Destroy( this.gameObject );

			audioSource = GetComponent<AudioSource>();
			audioSource.clip = MusicArray[Random.Range(0,MusicArray.Length)];
			audioSource.volume = GameController.Get ().TheGameVariables.Music;
			if (!GameController.Get ().TheGameVariables.Mute)
				audioSource.Play();
		}

		void Update()
		{

			if (Application.loadedLevelName == "OptionsMenu")
			{
				bool isMute = GameController.Get ().TheGameVariables.Mute;
				if (isMute && audioSource.isPlaying)
					audioSource.Stop();
				if (!isMute && !audioSource.isPlaying)
					audioSource.Play();
				audioSource.volume = GameController.Get ().TheGameVariables.Music;
			}
		}
		#endregion
	}
}
