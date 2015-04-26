using UnityEngine;
using System.Collections;

public class BackToMain : MonoBehaviour
{
	public void ToMainMenu ()
	{
		Application.LoadLevel("MainMenu");
	}
}
