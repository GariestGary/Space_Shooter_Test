using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Toolbox/Managers/Game Manager", fileName = "Game")]
public class GameManager: ManagerBase, IExecute
{
	[SerializeField] private AudioClip winSound;
	[SerializeField] private AudioClip looseSound;
	public GameObject Player { get; private set; }

	public bool IsPaused { get; private set; }

	private MessageManager msg => Toolbox.GetManager<MessageManager>();

	public void OnExecute()
	{
		msg.Subscribe(ServiceShareData.WIN, () => OnWin());
		msg.Subscribe(ServiceShareData.LOOSE, () => OnLoose());

		Objectives.Instance?.Initialize();

		SetPause(false);
	}

	public void ToMainMenu()
	{
		msg.Send(ServiceShareData.SCENE_CHANGE);
		SceneManager.LoadScene(0);
	}

	public void RestartLevel()
	{
		if (LevelLoader.Instance && Objectives.Instance && Objectives.Instance.CurrentObjective != null)
		{
			Objectives.Instance.SetObjective(LevelLoader.Instance.GetObjective(Objectives.Instance.CurrentObjective.LevelNumber));
		}
		DOTween.KillAll();
		SceneManager.LoadScene(1);
	}

	public void NextLevel()
	{
		if (Objectives.Instance.CurrentObjective == null) return;

		DOTween.KillAll();
		Objectives.Instance.SetObjective(LevelLoader.Instance.GetObjective(Objectives.Instance.CurrentObjective.LevelNumber + 1));
		SceneManager.LoadScene(1);
	}

	private void OnWin()
	{
		AudioPlayer.Instance?.Play(winSound);
		Time.timeScale = 0;
	}

	private void OnLoose()
	{
		AudioPlayer.Instance?.Play(looseSound);
		Time.timeScale = 0;
	}

	public void SetPause(bool value)
	{
		IsPaused = value;

		if(IsPaused)
		{
			Time.timeScale = 0;
			UI.Instance.SetPauseUI(true);
		}
		else
		{
			Time.timeScale = 1;
			UI.Instance.SetPauseUI(false);
		}
	}

	public void SetPlayer(GameObject player)
	{
		if (player == null) return;

		Player = player;

		msg.Send(ServiceShareData.PLAYER_SET);
	}
}