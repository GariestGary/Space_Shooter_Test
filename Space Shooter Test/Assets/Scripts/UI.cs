using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UI: Singleton<UI>, IAwake
{
	public GameObject PauseUI;
	[SerializeField] private GameObject WinUI;
	[SerializeField] private GameObject LooseUI;

	private MessageManager msg => Toolbox.GetManager<MessageManager>();

	public void OnAwake()
	{
		msg.Subscribe(ServiceShareData.WIN, () => OnWin());
		msg.Subscribe(ServiceShareData.LOOSE, () => OnLoose());
	}

	private void OnWin()
	{
		Debug.Log("Win");
	}

	private void OnLoose()
	{
		Debug.Log("Loose");
	}
}
