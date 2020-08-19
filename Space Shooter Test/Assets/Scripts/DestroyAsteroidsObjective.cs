using UnityEngine;

[CreateAssetMenu(menuName = "Game/Objectives/Destroy Asteroids")]
public class DestroyAsteroidsObjective : DefaultObjective
{
	[SerializeField] private int needToDestroy;

	private int currentCount;

	private MessageManager msg => Toolbox.GetManager<MessageManager>();

	public void CheckOne()
	{
		currentCount++;
		UI.Instance?.ProgressBar.UpdateBar(1);

		if (currentCount >= needToDestroy)
		{
			msg.Send(ServiceShareData.WIN);
			SetState(ObjectiveState.Finished);
		}
	}

	public override void Initialize()
	{
		currentCount = 0;
		UI.Instance?.ProgressBar.Initialize(needToDestroy);
		Debug.Log(UI.Instance);
		msg.Subscribe(ServiceShareData.ASTEROID_DESTROYED, () => CheckOne());
	}
}
