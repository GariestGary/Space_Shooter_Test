using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Projectile : MonoBehaviour, ITick, IAwake
{
	[SerializeField] private float speed;
	[SerializeField] private Vector2 colliderSize;
	[SerializeField] private LayerMask enemyLayer;

	private Transform t;
	private Camera camera;

	public bool Process => gameObject.activeSelf;

	public void OnAwake()
	{
		t = transform;
		camera = Camera.main;

		Toolbox.GetManager<UpdateManager>().Add(this);
	}

	public void OnTick()
	{
		Move();
		CollideCheck();
	}

	private void Move()
	{
		t.position += t.up * speed * Time.deltaTime;

		float yPos = camera.WorldToViewportPoint(t.position).y;

		if(yPos > 2)
		{
			ObjectPooler.Instance.Despawn(gameObject);
		}
	}

	private void CollideCheck()
	{
		Collider2D[] enemies = Physics2D.OverlapBoxAll(t.position, colliderSize, 0, enemyLayer);

		for (int i = 0; i < enemies.Length; i++)
		{
			if (enemies[i].TryGetComponent(out IEnemy enemy))
			{
				enemy.Kill();
				ObjectPooler.Instance.Despawn(gameObject);
			}
		}
	}

#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(transform.position, colliderSize);
	}
#endif
}