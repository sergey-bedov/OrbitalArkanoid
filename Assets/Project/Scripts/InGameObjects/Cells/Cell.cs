using UnityEngine;
using System.Collections;
using SB.Controllers;


namespace SB.InGameObjects.Cells
{

	public enum CellType
	{
		Brick,
		Hexagon,
		Sector
	}

	[RequireComponent (typeof (Collider2D), typeof (Rigidbody2D))]
	public class Cell : MonoBehaviour
	{
		public string Name;
		public string Description;
		public CellType TheCellType;

		public int Damage;
		public Sprite[] CellStates;
		public AudioClip DamageSound;
		public AudioClip DestroySound;

		private SpriteRenderer spriteRenderer;
		private AudioSource audioSource;

		void Awake ()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
			if (CellStates.Length > 0)
			{
				spriteRenderer.sprite = CellStates[Damage];
			}
			audioSource = gameObject.AddComponent<AudioSource>();
		}

		void OnCollisionEnter2D(Collision2D coll)
		{
			if (coll.gameObject.tag == "Ball")
			{
				// Damage OR Destroy the Block
				if (Damage < CellStates.Length - 1) // IF DAMAGED
				{
					//PLAY SOUND
					if (!DamageSound || !DestroySound)
						audioSource.PlayOneShot(DamageSound);
					else
						audioSource.PlayOneShot(SoundController.Get().GetSound());

					//DAMAGE The Block
					Damage++;
					spriteRenderer.sprite = CellStates[Damage];
				}
				else // IF DESTROYED
				{
					//PLAY SOUND
					if (DestroySound)
						audioSource.PlayOneShot(DestroySound);
					else
						audioSource.PlayOneShot(SoundController.Get().GetSound());

					//DESTROY The Block
					gameObject.SetActive(false);
					gameObject.GetComponentInParent<Level>().UpdateBlocksLeft(); // Update Level Progress Info
				}
			}
		}
	}
}
