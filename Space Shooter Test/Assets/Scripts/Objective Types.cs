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
			LevelLoader.Instance.GetObjective(levelNumber + 1).SetState(ObjectiveState.Opened);
		}
	}

	public void SetNumber(int num)
	{
		levelNumber = num;
	}
}

[CreateAssetMenu(fileName = "Game/Objectives/Destroy Asteroids")]
public class DestroyAsteroids : DefaultObjective
{
	[SerializeField] private int needToDestroy;

	private int currentCount;

	private MessageManager msg => Toolbox.GetManager<MessageManager>();

	public void CheckOne()
	{
		currentCount++;

		if (currentCount >= needToDestroy)
		{
			msg.Send(ServiceShareData.WIN);
			SetState(ObjectiveState.Finished);
		}
	}

	public override void Initialize()
	{
		currentCount = 0;

		msg.Subscribe(ServiceShareData.ASTEROID_DESTROYED, () => CheckOne());
	}
}

[CreateAssetMenu(menuName = "Game/Objectives/Live Over Time")]
public class LiveOverTime: DefaultObjective
{
	[SerializeField] private int secondsNeedToLive;
	private MessageManager msg => Toolbox.GetManager<MessageManager>();

	IDisposable timer;

	public override void Initialize()
	{
		timer = Observable.Timer(new TimeSpan(0, 0, secondsNeedToLive)).Subscribe(_ => { msg.Send(ServiceShareData.WIN); SetState(ObjectiveState.Finished); });
		msg.Subscribe(ServiceShareData.LOOSE, () => timer.Dispose());
	}
}

public enum ObjectiveState
{
	Locked,
	Opened,
	Finished
}
