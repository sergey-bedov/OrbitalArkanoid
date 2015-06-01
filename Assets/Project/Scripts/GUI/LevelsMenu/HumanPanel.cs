using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HumanPanel : MonoBehaviour
{
	public Image Human;
	public Image[] Level_01;
	public Image[] Level_02;
	public Image[] Level_03;
	public Image[] Level_04;
	public Image[] Level_05;
	public Image[] Level_06;
	public Image[] Level_07;
	public Image[] Level_08;
	public Image[] Level_09;
	public Image[] Level_10;

	private List<Image[]> levels;

	void Awake()
	{
		levels = new System.Collections.Generic.List<Image[]>();
		levels.Add(Level_01); levels.Add(Level_02); levels.Add(Level_03); levels.Add(Level_04); levels.Add(Level_05);
		levels.Add(Level_06); levels.Add(Level_07); levels.Add(Level_08); levels.Add(Level_09); levels.Add(Level_10);

		PapulatePanel(GameVariables.TopLevel);
	}

	public void PapulatePanel(int num)
	{
		if (levels != null)
		for (int i = 0; i < levels.Count; i++)
		{
			if (i == num-1)
			{
				foreach (Image img in levels[i])
				{
					img.color = new Color(1F,1F,1F,1F);
				}
			}
			else
			{
				foreach (Image img in levels[i])
				{
					img.color = new Color(1F,1F,1F,0.2F);
				}
			}
		}
	}
}
