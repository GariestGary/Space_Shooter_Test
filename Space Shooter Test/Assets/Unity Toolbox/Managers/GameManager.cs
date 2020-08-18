using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Toolbox/Managers/Game Manager", fileName = "Game")]
public class GameManager: ManagerBase, IExecute
{
	public GameObject Player { get; private set; }

	public bool IsPaused { get; private set; }

	private MessageManager msg => Toolbox.GetManager<MessageManager>();

	public void OnExecute()
	{
		msg.Subscribe(ServiceShareData.WIN, () => OnWin());
		msg.Subscribe(ServiceShareData.LOOSE, () => OnLoose());

		Objectives.Instance?.Initialize();
	}

	private void OnWin()
	{
		Time.timeScale = 0;
	}

	private void OnLoose()
	{
		Time.timeScale = 0;
	}

	public void SetPause(bool value)
	{
		IsPaused = value;

		if(IsPaused)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}

	public void SetPlayer(GameObject player)
	{
		Player = player;
	}
}