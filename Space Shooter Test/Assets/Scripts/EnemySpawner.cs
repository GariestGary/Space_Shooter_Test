using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IAwake
{
	[SerializeField] private int intervalInMilliseconds;
	[SerializeField] private string enemyPoolTag;

	private GameManager game => Toolbox.GetManager<GameManager>();

	private IDisposable timer;

	public void OnAwake()
	{
		timer = Observable.Interval(new TimeSpan(0, 0, 0, 0, intervalInMilliseconds)).Subscribe(_ => SpawnEnemy());
		Toolbox.AddDisposable(timer);
	}

	public void SpawnEnemy()
	{
		if (!game.Player) return;

		Vector2 viewPos = new Vector2(UnityEngine.Random.value, 2);

		Vector3 spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(viewPos.x, viewPos.y, 0));
		spawnPos.z = 0;

		ObjectPooler.Instance.GetObject(enemyPoolTag, spawnPos, Quaternion.identity);
	}
}
