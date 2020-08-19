using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ShootComponent : MonoBehaviour, IAwake, ISceneChange, ITick
{
	[SerializeField] private AudioClip shootSound;
	[SerializeField] private string projectilePoolTag;
	[SerializeField] private float shootInterval;

	private InputManager input => Toolbox.GetManager<InputManager>();
	private MessageManager msg => Toolbox.GetManager<MessageManager>();

	public bool Process => gameObject.activeSelf;

	private bool canShoot;
	private float currentInterval;

	private Transform t;
	public void OnAwake()
	{
		canShoot = true;
		t = transform;
		input.OnClick += Shoot;

		msg.Subscribe(ServiceShareData.LOOSE, () => { canShoot = false; });
		Toolbox.GetManager<UpdateManager>().Add(this);
	}

	public void OnTick()
	{
		if (currentInterval <= 0) return;

		currentInterval -= Time.deltaTime;
	}

	public void Shoot()
	{
		if (!canShoot) return;

		if (currentInterval > 0) return;

		AudioPlayer.Instance?.Play(shootSound);

		currentInterval = shootInterval;
		ObjectPooler.Instance.GetObject(projectilePoolTag, t.position, Quaternion.identity);
	}

	public void OnSceneChange()
	{
		input.OnClick -= Shoot;
	}

}
