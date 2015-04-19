using UnityEngine;
using System.Collections;

namespace SB.Controllers
{
	public class PlayerController : MonoBehaviour
	{
		Player ThePlayer;
		GameObject ThePlayerGO;

		#region Access Instance Anywhere
		private static PlayerController playerControl;
		public static PlayerController Get()
		{
			if( playerControl != null )
				return playerControl;
			else
			{
				GameObject obj = new GameObject("PlayerController");
				obj.transform.SetParent(GameController.Get().transform);
				obj.tag = "Controller";
				playerControl = obj.AddComponent<PlayerController>();
				return playerControl;
			}
		}
		void Awake() 
		{
			if( playerControl == null )
				playerControl = this;
			else
				GameObject.Destroy( this.gameObject );

			ThePlayerGO = GameObject.FindObjectOfType(typeof(Player)) as GameObject;
			if (ThePlayerGO == null)
			{
				ThePlayerGO = new GameObject("Player");
				ThePlayer = ThePlayerGO.AddComponent<Player>();
			}
			else
				ThePlayer = GetComponent<Player>();
		}
		#endregion
	}

}
