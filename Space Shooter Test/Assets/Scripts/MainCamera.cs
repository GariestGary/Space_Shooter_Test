using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MainCamera : MonoBehaviour, IAwake
{
	[SerializeField] private float shakeStrength;

	public void OnAwake()
	{
		Toolbox.GetManager<MessageManager>().Subscribe(ServiceShareData.SHIP_HIT, () =>
		{
			transform.DOShakePosition(0.5f, shakeStrength);
		});
	}
}
