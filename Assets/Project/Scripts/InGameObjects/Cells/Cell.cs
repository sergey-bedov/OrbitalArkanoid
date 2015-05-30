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
			//	spriteRenderer.sprite = CellStates[Damage];
				spriteRenderer.material.SetTexture("_BgTex", CellStates[Damage].texture);
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
					//spriteRenderer.sprite = CellStates[Damage];
					if (spriteRenderer.material.shader.name == "Sprites/Default")
						spriteRenderer.sprite = CellStates[Damage];
					else
						spriteRenderer.material.SetTexture("_BgTex", CellStates[Damage].texture);
				}
				else // IF DESTROYED
				{
					//PLAY SOUND
					if (DestroySound)
						audioSource.PlayOneShot(DestroySound);
					else
						audioSource.PlayOneShot(SoundController.Get().GetSound());

					//DESTROY The Block
					StartCoroutine(CrashTheBlock());
				}
			}
		}
		IEnumerator CrashTheBlock() // TODO make block crash amimations or something like that
		{
			GameObject particleBoomTest = Instantiate (Resources.Load("Prefabs/Effects/ParticleBoomTest", typeof(GameObject))) as GameObject;
			particleBoomTest.transform.position = this.transform.position;
			yield return new WaitForSeconds(0.2F);
			gameObject.SetActive(false);
			gameObject.GetComponentInParent<Level>().UpdateBlocksLeft(); // Update Level Progress Info
		}
	}
}
