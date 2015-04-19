using UnityEngine;
using System.Collections;

namespace SB.InGameObjects.Cells
{
	public enum CellType
	{
		Brick,
		Hexagon,
		Sector
	}

	public class Cell : MonoBehaviour
	{
		public string Name;
		public string Description;
		public CellType TheCellType;

		public int Damage;
		public Sprite[] CellStates;
		private SpriteRenderer spriteRenderer;

		void Awake ()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
			if (CellStates.Length > 0)
			{
				spriteRenderer.sprite = CellStates[Damage];
			}
		}

		void OnCollisionEnter2D(Collision2D coll)
		{
			if (coll.gameObject.tag == "Ball")
			{
				if (Damage < CellStates.Length - 1)
				{
					Damage++;
					spriteRenderer.sprite = CellStates[Damage];
				}
				else
				{
					Destroy(gameObject);
				}
			}
		}
	}
}
