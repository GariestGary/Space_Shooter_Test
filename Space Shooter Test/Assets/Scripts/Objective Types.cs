using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEditor;
using UnityEngine;

public class DefaultObjective : ScriptableObject, IObjective
{
	public ObjectiveState State => currentState;
	[SerializeField] protected ObjectiveState currentState;

	public int LevelNumber => levelNumber;
	protected int levelNumber;

	
	public virtual void Initialize()
	{
		
	}

	public void SetState(ObjectiveState state)
	{
		currentState = state;

		if (currentState == ObjectiveState.Finished)
		{
			LevelLoader.Instance.GetObjective(levelNumber + 1)?.SetState(ObjectiveState.Opened);
		}

		Save();
	}

	public void SetNumber(int num)
	{
		levelNumber = num;
	}

	public void Save()
	{
		PlayerPrefs.SetInt(levelNumber.ToString() + " level", (int)State);
	}
}

public enum ObjectiveState
{
	Locked,
	Opened,
	Finished
}
