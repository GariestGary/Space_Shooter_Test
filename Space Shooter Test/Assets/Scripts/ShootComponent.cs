using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ShootComponent : MonoBehaviour, IAwake
{
	[SerializeField] private string projectilePoolTag;

	private InputManager input => Toolbox.GetManager<InputManager>();
	private MessageManager msg => Toolbox.GetManager<MessageManager>();

	private bool canShoot;

	private Transform t;
	public void OnAwake()
	{
		canShoot = true;
		t = transform;
		input.OnClick += Shoot;

		msg.Subscribe(ServiceShareData.LOOSE, () => { canShoot = false; });
	}



	public void Shoot()
	{
		if (!canShoot) return;

		ObjectPooler.Instance.GetObject(projectilePoolTag, t.position, Quaternion.identity);
	}

	private void OnApplicationQuit()
	{
		if (input) input.OnClick -= Shoot;
	}
}
