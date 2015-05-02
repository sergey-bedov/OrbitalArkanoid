﻿using UnityEngine;
using System.Collections;
using SB.InGameObjects;

namespace SB.Controllers
{
	public class BoardController : MonoBehaviour 
	{
		[SerializeField]
		private Board[] boards;
		private Vector2 halfScreen;

		private bool isKeepMoving;
		private float keepMovingSpeed;

		#region Access Instance Anywhere
		private static BoardController boardControl;
		public static BoardController Get()
		{
			if( boardControl != null )
				return boardControl;
			else
			{
				GameObject obj = new GameObject("BoardController");
				obj.transform.SetParent(GameController.Get().transform);
				obj.tag = "Controller";
				boardControl = obj.AddComponent<BoardController>();
				return boardControl;
			}
		}
		void Awake() 
		{
			if( boardControl == null )
				boardControl = this;
			else
				GameObject.Destroy( this.gameObject );

			//GetBoards();

			halfScreen = new Vector2( Screen.width/2, Screen.height/2 );

			isKeepMoving = false;
			keepMovingSpeed = 0;
		}
		#endregion

//		private void GetBoards()
//		{
//			boards = new Board[0];
//			foreach (GameObject boardGO in GameObject.FindGameObjectsWithTag("Board"))
//			{
//				Board board = boardGO.GetComponent<Board>();
//				boards = ArrayTools.PushLast(boards, board);
//			}
//		}

		private bool isOnPause = false;
		public void Pause()
		{
			isOnPause = true;
		}
		public void UnPause()
		{
			isOnPause = false;
		}

		public int ControlType = 0;
		void Update ()
		{
			if (!isOnPause)
			{
//				if (boards == null) GetBoards();
				if (Input.anyKey) KeyboardControl();
				if (Input.GetButton("Fire1"))
				{
					if (ControlType == 0)
						MouseControlArrows();
					if (ControlType == 1)
						MouseControlOverScteenQuarter();
					if (ControlType == 2)
						MouseControlOverScteenCircleBoard();
					if (ControlType == 3)
						MouseControlOverScteenCircleRelative();
				}
				if (isKeepMoving)
					MoveBoards(keepMovingSpeed);
			}
		}

		private void KeyboardControl ()
		{
			float h = Input.GetAxis("Horizontal");
			MoveBoards(h);
		}
		public void MoveBoards (float speed)
		{
			if (boards != null)
				foreach (Board board in boards)
				{
					board.MoveBoard(speed*board.Speed);
				}
		}
		public void ConstantMove (float speed)
		{
			keepMovingSpeed = speed;
			if (speed == 0)
				isKeepMoving = false;
			else
				isKeepMoving = true;
		}
		void MouseControlArrows()
		{

		}

		#region Mouse Control Over Scteen Quarter

		float mouseDistance = 0;
		
		private Vector3 lastPosition;
		private bool trackMouse = false;
		
		void MouseControlOverScteenQuarter ()
		{
			if (Input.GetButtonDown ("Fire1"))
			{
				trackMouse = true;
				lastPosition = Input.mousePosition - (Vector3)halfScreen;
			}
			
			if (Input.GetButtonUp ("Fire1"))
			{
				trackMouse = false;
				Debug.Log ("Mouse moved " + mouseDistance + " while button was down.");
				mouseDistance = 0;
			}
			
			if (trackMouse)
			{
				Vector3 newPosition = Input.mousePosition - (Vector3)halfScreen;
				// If you just want the x-axis:
				// mouseDistance += Mathf.Abs (newPosition.x - lastPosition.x);
				// If you just want the y-axis,change newPosition.x to newPosition.y and lastPosition.x to lastPosition.y
				// If you want the entire distance moved (not just the X-axis, use:
				// mouseDistance += (newPosition - lastPosition).magnitude;
				Vector3 distanceVector = newPosition-lastPosition;
				float distance = distanceVector.magnitude;
				float sign;


				if (Mathf.Abs(distanceVector.x) > Mathf.Abs(distanceVector.y))
				{
					if (newPosition.y > 0)
					{
						sign = -Mathf.Sign(distanceVector.x);
					}
					else
					{
						sign = Mathf.Sign(distanceVector.x);
					}
				}
				else
				{
					if (newPosition.x > 0)
					{
						sign = Mathf.Sign(distanceVector.y);
					}
					else
					{
						sign = -Mathf.Sign(distanceVector.y);
					}
				}
				mouseDistance = sign * Vector3.Distance(newPosition, lastPosition);
				lastPosition = newPosition;
			}
			foreach (Board board in boards)
			{
				board.MoveBoard(mouseDistance*board.Speed/5);
			}
		}

		#endregion

		#region Mouse Control Over Scteen Circle Board
		private void MouseControlOverScteenCircleBoard()
		{
			Vector3 newPosition = Input.mousePosition - (Vector3)halfScreen;
			float angle = Vector3.Angle(newPosition, Vector3.down);
			if (newPosition.x < 0) angle = -angle;
			foreach (Board board in boards)
			{
				board.LerpBoard(angle);
			}
		}
		#endregion

		#region Mouse Control Over Scteen Circle Relative
		private float lastAngle;
		private float newAngle;
		private float angle;
	//	private bool trackMouse = false;
		void MouseControlOverScteenCircleRelative()
		{
			if (Input.GetButtonDown ("Fire1"))
			{
				trackMouse = true;
				lastPosition = Input.mousePosition - (Vector3)halfScreen;
				lastAngle = Vector3.Angle(lastPosition, Vector3.down);
			}
			
			if (Input.GetButtonUp ("Fire1"))
			{
				trackMouse = false;
			}
			
			if (trackMouse)
			{
				Vector3 newPosition = Input.mousePosition - (Vector3)halfScreen;
				float newAngle = Vector3.Angle(newPosition, Vector3.down);
				angle = newAngle - lastAngle;
//				print(angle);
				lastAngle = newAngle;
				if (newPosition.x < 0)
					angle = - angle;
			}
			if (boards != null)
				foreach (Board board in boards)
				{
					board.LerpBoardRelatively(2*angle);
				}
		}
		#endregion

		#region Add/Remove Board from Array
		public void BoardCreated(Board board)
		{
			if (boards == null) boards = new Board[0];
			boards = ArrayTools.PushLast(boards, board);
		}
		public void BoardDestroyed(Board board)
		{
			boards = ArrayTools.Remove(boards, board);
		}
		public void Cleanup()
		{
			boards = new Board[0];
		}
		#endregion
	}
}