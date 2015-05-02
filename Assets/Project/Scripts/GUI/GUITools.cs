using UnityEngine;
using System.Collections;

public class GUITools : MonoBehaviour
{
	public void WhenDrag()
	{
		Vector3 toDrag = new Vector3 (Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height, 0);
		transform.position = toDrag;
	}

	public GameObject something;
	public void HideShowSomething()
	{
		print ("Hide/Show " + something.name);
		something.SetActive(!something.activeSelf);
	}
}
