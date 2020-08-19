using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, IAwake, ISceneChange
{
	private Text healthText;
	private IDisposable observer;

	public void OnAwake()
	{
		healthText = GetComponent<Text>();

		Toolbox.GetManager<MessageManager>().Subscribe(ServiceShareData.PLAYER_SET, () =>
		{
			observer = Toolbox.GetManager<GameManager>().Player.GetComponent<Ship>().ObserveEveryValueChanged(x => x.CurrentHealth).Subscribe(x =>
			{
				healthText.text = "Health: " + x;
			});
		});
	}

	public void OnSceneChange()
	{
		observer.Dispose();
	}
}
