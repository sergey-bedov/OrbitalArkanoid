using UnityEngine;
using System.Collections;
using SB.Controllers;

namespace SB.InGameObjects
{
	public class Ball : MonoBehaviour
	{
		public string Name;
		public string Description;

		public float Speed = 2F;

		private Rigidbody2D rigidBody;

		public Transform trans;

		private GameObject particleMoveTest;

		void Awake ()
		{
			rigidBody = GetComponent<Rigidbody2D>();
			GameObject Director = new GameObject("Director");
			Director.transform.position = transform.position;
			Director.transform.rotation = transform.rotation;
			Director.transform.SetParent(transform);
			particleMoveTest = Instantiate (Resources.Load("Prefabs/Effects/ParticleMoveTest", typeof(GameObject))) as GameObject;
			particleMoveTest.transform.position = Director.transform.position;
			particleMoveTest.transform.SetParent(Director.transform);

			trans = Director.GetComponent<Transform>();
			FaceDirection(Vector3.down);
			BallController.Get().BallCreated(this);
		}

		void Start ()
		{
			// FOR NOW:
			float sliderSpeed;
			if (GameObject.Find("BallSpeedSlider") != null)
				sliderSpeed = GameObject.Find("BallSpeedSlider").GetComponent<UnityEngine.UI.Slider>().value;
			else
				sliderSpeed = 0.5F;
			Speed = Speed * sliderSpeed + 0.5F;

			if (!GameController.Get().IsOnPause)
				rigidBody.velocity = trans.up * Speed;

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
			print("Ball Collided! Bounce with Speed of " + Speed);
			FaceDirection(rigidBody.velocity);
			SetVelocity(Speed);
			particleMoveTest.GetComponent<ParticleSystem>().Clear();
		//	}
		}
	}
}
