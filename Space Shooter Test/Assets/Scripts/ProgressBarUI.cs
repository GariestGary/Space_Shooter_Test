using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI: MonoBehaviour
{
	[SerializeField] private Slider slider;

	private int currentProgress;
	private int maxProgress;

	public void UpdateBar(int value)
	{
		currentProgress += value;

		slider.value = (float)currentProgress / (float)maxProgress;
	}

	public void Initialize(int max)
	{
		slider.value = 0;
		currentProgress = 0;
		maxProgress = max;
	}
}

