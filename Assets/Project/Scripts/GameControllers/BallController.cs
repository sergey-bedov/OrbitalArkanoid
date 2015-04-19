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
			foreach (GameObject ballGO in GameObject.FindGameObjectsWithTag("Ball"))
			{
				Ball ball = ballGO.GetComponent<Ball>();
				balls = ArrayTools.PushLast(balls, ball);
			}
		}
		#endregion

		public void BallDestroyed(Ball ball)
		{
			balls = ArrayTools.Remove(balls, ball);
			if (balls.Length == 0)
			{
				GameController.Get().LostBalls();

				// FOR NOW:
				GameObject tempBall = Instantiate (Resources.Load("Prefabs/Balls/TestBall", typeof(GameObject))) as GameObject;
				tempBall.transform.localScale = Random.Range(0.2F, 1F) * Vector3.one;
				tempBall.GetComponent<Ball>().Speed = Random.Range(2F, 10F);
			}
		}
	}
}
