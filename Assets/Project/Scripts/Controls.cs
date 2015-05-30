using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Controls : MonoBehaviour
{
	public Button LeftCW;
	public Button LeftCCW;
	public Button RightCW;
	public Button RightCCW;

	[SerializeField]
	private Sprite cwIdle;
	[SerializeField]
	private Sprite ccwIdle;
	[SerializeField]
	private Sprite cwActive;
	[SerializeField]
	private Sprite ccwActive;
	[SerializeField]
	private Sprite cwPassive;
	[SerializeField]
	private Sprite ccwPassive;

	public void Idle()
	{
		LeftCW.image.sprite = cwIdle;
		LeftCCW.image.sprite = ccwIdle;
		RightCW.image.sprite = cwIdle;
		RightCCW.image.sprite = ccwIdle;
	}
	public void MoveCW()
	{
		LeftCW.image.sprite = cwActive;
		LeftCCW.image.sprite = ccwPassive;
		RightCW.image.sprite = cwActive;
		RightCCW.image.sprite = ccwPassive;
	}
	public void MoveCCW()
	{
		LeftCW.image.sprite = cwPassive;
		LeftCCW.image.sprite = ccwActive;
		RightCW.image.sprite = cwPassive;
		RightCCW.image.sprite = ccwActive;
	}
}
