using UnityEngine;
using System.Collections;

namespace SB.InGameObjects
{
	public class Board : MonoBehaviour
	{
		public string Name;
		public string Description;
		public float Radius = 4.5F;
		public float Angle = 0F;

		public float Speed = 2F;

		private Transform trans;

		void Awake ()
		{
			trans = GetComponent<Transform>();
			ResetBoard();
		}

		public void PutBoard (float radius, float angle)
		{
			trans.rotation = Quaternion.AngleAxis(-angle, Vector3.back);
			trans.position = -trans.up * radius;
		}
		public void MoveBoard (float speed)
		{
			transform.RotateAround(Vector3.zero, Vector3.back, -speed);
		}
		public void ResetBoard ()
		{
			PutBoard (Radius, Angle);
		}
	}
}
