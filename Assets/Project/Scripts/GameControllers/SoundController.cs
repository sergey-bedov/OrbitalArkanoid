using UnityEngine;
using System.Collections;

namespace SB.Controllers
{
	[RequireComponent (typeof(AudioSource))]
	public class SoundController : MonoBehaviour
	{
		public AudioClip [] SoundArray;
		AudioSource audioSource;

		#region Access Instance Anywhere
		private static SoundController soundControl;
		public static SoundController Get()
		{
			if( soundControl != null )
				return soundControl;
			else
			{
			//	GameObject obj = new GameObject("SoundController");
				GameObject obj = (GameObject)Instantiate(Resources.Load("Prefabs/Sound&Music/SoundController"));
				obj.name = "SoundController";
				obj.transform.SetParent(GameController.Get().transform);
				obj.tag = "Controller";
				soundControl = obj.GetComponent<SoundController>();
				return soundControl;
			}
		}
		void Awake() 
		{
			DontDestroyOnLoad(transform.gameObject);
			if( soundControl == null )
				soundControl = this;
			else
				GameObject.Destroy( this.gameObject );
			
			audioSource = GetComponent<AudioSource>();
			audioSource.clip = SoundArray[0];
			audioSource.volume = GameController.Get ().TheGameVariables.SoundEffects;
		}
		#endregion
		public void PlaySound(int clipNumber)
		{
			Debug.Log("Play Ball Hit Sound.");
			audioSource.PlayOneShot(SoundArray[clipNumber]);
		}
		public void PlaySound()
		{
			PlaySound(0);
		}
		public AudioClip GetSound(int clipNumber)
		{
			return SoundArray[clipNumber];
		}
		public AudioClip GetSound()
		{
			return GetSound(0);
		}
	}
}