using UnityEngine;
using System.Collections;

namespace SB.InGameObjects
{
	public class Board : MonoBehaviour
	{
		public string Name;
		public string Description;
		public float Angle = 0F;

		public float Speed = 5F;

		private Transform trans;

		void Awake ()
		{
			trans = GetComponent<Transform>();
			ResetBoard();
		}

		public void PutBoard (float angle)
		{
			trans.rotation = Quaternion.AngleAxis(-angle, Vector3.back);

		}

		public UnityEngine.UI.Slider SpeedSlider; // FOR NOW

		public void MoveBoard (float speed)
		{
			transform.RotateAround(Vector3.zero, Vector3.back, -speed * SpeedSlider.value);
		}

		public void LerpBoard (float angle)
		{
			float speed = 1000F * SpeedSlider.value + 5F; 
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), Time.deltaTime * speed);
		}
		public void LerpBoardRelatively (float angle)
		{
			float speed = 1000F * SpeedSlider.value + 5F;
			print (transform.rotation.z);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, transform.eulerAngles.z	 + angle)), Time.deltaTime * speed);
		}
		public void ResetBoard ()
		{
			PutBoard (Angle);
		}
	}
}
