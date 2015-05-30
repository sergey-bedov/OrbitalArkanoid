using UnityEngine;
using System.Collections;

public class ControlsButton : MonoBehaviour
{
	private Controls controls;
	void Awake()
	{
		controls = GetComponentInParent<Controls>();
	}
	public void Idle()
	{
		controls.Idle();
	}
	public void MoveCW()
	{
		controls.MoveCW();
	}
	public void MoveCCW()
	{
		controls.MoveCCW();
	}
}
