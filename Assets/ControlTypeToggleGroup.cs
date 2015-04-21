using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlTypeToggleGroup : MonoBehaviour
{
	ToggleGroup tg;
	UnityEngine.UI.Toggle.ToggleEvent te;

	void Awake()
	{
		tg = GetComponent<ToggleGroup>();

	}
}
