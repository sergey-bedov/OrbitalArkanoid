using UnityEngine;
using System.Collections;
using SB.Controllers;

namespace SB.InGameObjects
{
	public class Ball : MonoBehaviour
	{
		public string Name;
		public string Description;

		public float Speed = 3F;

		private Rigidbody2D rigidBody;
		private Transform trans;

		void Awake ()
		{
			rigidBody = GetComponent<Rigidbody2D>();
			trans = GetComponent<Transform>();
		}

		void Start ()
		{
			rigidBody.velocity = Vector3.down * Speed;
		}
		
		void Update ()
		{
			if (OutOfGameArea())
			{
				BallController.Get().BallDestroyed(this);
				Destroy(this.gameObject);
			}
		}
		
		float gameArea = 5F;
		bool OutOfGameArea ()
		{
			if ((Mathf.Abs(trans.position.x) > gameArea) || (Mathf.Abs(trans.position.y) > gameArea))
				return true;
			else
				return false;
		}
	}
}
