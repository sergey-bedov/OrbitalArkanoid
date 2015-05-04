using UnityEngine;
using System.Collections;
using SB.InGameObjects;

namespace SB.Controllers
{
	public class BallController : MonoBehaviour
	{
		[SerializeField]
		private Ball[] balls;
		
		#region Access Instance Anywhere
		private static BallController ballControl;
		public static BallController Get()
		{
			if( ballControl != null )
				return ballControl;
			else
			{
				GameObject obj = new GameObject("BallController");
				obj.transform.SetParent(GameController.Get().transform);
				obj.tag = "Controller";
				ballControl = obj.AddComponent<BallController>();
				return ballControl;
			}
		}
		void Awake() 
		{
			if( ballControl == null )
				ballControl = this;
			else
				GameObject.Destroy( this.gameObject );

			balls = new Ball[0];
		}
		#endregion

		public void OnPause()
		{
			SetBallsVelocity(0);
		}

		public void OnUnpause()
		{
			SetBallsVelocity(8);
		}
		public void SetBallsVelocity(Vector2 velocityVector)
		{
			foreach (Ball ball in balls)
				ball.SetVelocity(velocityVector);
		}
		public void SetBallsVelocity(Vector3 velocityVector)
		{
			SetBallsVelocity((Vector2)velocityVector);
		}
		public void SetBallsVelocity(float velocity)
		{
			foreach (Ball ball in balls)
				ball.SetVelocity(velocity);
		}

		#region Add/Remove Ball from Array
		public void BallCreated(Ball ball)
		{
			balls = ArrayTools.PushLast(balls, ball);
		}
		public void BallDestroyed(Ball ball)
		{
			balls = ArrayTools.Remove(balls, ball);
			if (balls.Length == 0)
			{
				GameController.Get().LostBalls();

				// FOR NOW:
				GameObject tempBall = Instantiate (Resources.Load("Prefabs/Balls/TestBall", typeof(GameObject))) as GameObject;
				tempBall.transform.localScale = Random.Range(0.7F, 0.7F) * Vector3.one;
			//	tempBall.GetComponent<Ball>().Speed = Random.Range(2F, 10F);
			}
		}
		public void Cleanup()
		{
			balls = new Ball[0];
		}
		#endregion
	}
}
