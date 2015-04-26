using UnityEngine;
using System.Collections;
using SB.Controllers;

namespace SB.InGameObjects
{
	public class Ball : MonoBehaviour
	{
		public string Name;
		public string Description;

		public float Speed = 4F;

		private Rigidbody2D rigidBody;

		public Transform trans;

		void Awake ()
		{
			rigidBody = GetComponent<Rigidbody2D>();
			GameObject Director = new GameObject("Director");
			Director.transform.position = transform.position;
			Director.transform.rotation = transform.rotation;
			Director.transform.SetParent(transform);

			trans = Director.GetComponent<Transform>();
			FaceDirection(rigidBody.velocity);
			BallController.Get().BallCreated(this);
		}

		void Start ()
		{
			// FOR NOW:
			UnityEngine.UI.Slider SpeedSlider = GameObject.Find("BallSpeedSlider").GetComponent<UnityEngine.UI.Slider>();
			Speed = Speed * SpeedSlider.value + 2F;

			rigidBody.velocity = Vector3.down * Speed;
		}

		public void SetVelocity(Vector2 velocityVector)
		{
			rigidBody.velocity = velocityVector;
		}
		public void SetVelocity(Vector3 velocityVector)
		{
			SetVelocity((Vector2)velocityVector);
		}
		public void SetVelocity(float velocity)
		{
			rigidBody.velocity = trans.up.normalized * velocity;
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

		public void FaceDirection(Vector2 faceVector)
		{
			float angle = Mathf.Atan2(-faceVector.x, faceVector.y) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			trans.rotation = q;
		}

		void OnCollisionExit2D(Collision2D coll)
		{
		//	if (coll.gameObject.tag == "Ball")
		//	{
			FaceDirection(rigidBody.velocity);
		//	}
		}
	}
}
