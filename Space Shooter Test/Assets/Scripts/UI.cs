using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UI: Singleton<UI>, IAwake
{
	public UI()
	{
		destroyOnLoad = true;
	}

	public void SetPause(bool value) => Toolbox.GetManager<GameManager>().SetPause(value);
	public void GoToMainMenu() => Toolbox.GetManager<GameManager>().ToMainMenu();
	public void Restart() => Toolbox.GetManager<GameManager>().RestartLevel();
	public void NextLevel() => Toolbox.GetManager<GameManager>().NextLevel();

	public ProgressBarUI ProgressBar => progressBar;

	[SerializeField] private ProgressBarUI progressBar;
	[SerializeField] private GameObject PauseUI;
	[SerializeField] private GameObject GameUI;
	[SerializeField] private GameObject WinUI;
	[SerializeField] private GameObject LooseUI;

	private MessageManager msg => Toolbox.GetManager<MessageManager>();

	public void OnAwake()
	{
		msg.Subscribe(ServiceShareData.WIN, () => OnWin());
		msg.Subscribe(ServiceShareData.LOOSE, () => OnLoose());
	}

	public void SetPauseUI(bool value)
	{
		PauseUI.SetActive(value);
	}

	private void OnWin()
	{
		GameUI?.SetActive(false);
		WinUI?.SetActive(true);
		LooseUI?.SetActive(false);
	}

	private void OnLoose()
	{
		GameUI?.SetActive(false);
		WinUI?.SetActive(false);
		LooseUI?.SetActive(true);
	}

}
