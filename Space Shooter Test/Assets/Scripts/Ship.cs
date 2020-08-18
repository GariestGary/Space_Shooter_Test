using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour, IAwake, ITick
{
    [SerializeField] private int maxHealth;
	[SerializeField] private LayerMask enemyLayer;
	[SerializeField] private Vector2 colliderSize;

	private int currentHealth;

	private MessageManager msg => Toolbox.GetManager<MessageManager>();

	public bool Process => gameObject.activeSelf;

	private Transform t;

	public void OnAwake()
	{
		t = transform;
		currentHealth = maxHealth;

		Toolbox.GetManager<UpdateManager>().Add(this);
		Toolbox.GetManager<GameManager>().SetPlayer(gameObject);
	}

	public void OnHit()
	{
		currentHealth--;

		if(currentHealth <= 0)
		{
			msg.Send(ServiceShareData.LOOSE);
		}

		Debug.Log("Hitted");
	}

	private void CollideCheck()
	{
		Collider2D[] enemies = Physics2D.OverlapBoxAll(t.position, colliderSize, 0, enemyLayer);

		for (int i = 0; i < enemies.Length; i++)
		{
			if(enemies[i].TryGetComponent(out IEnemy enemy))
			{
				enemy.Kill();
				OnHit();
			}
		}
	}

	public void OnTick()
	{
		CollideCheck();
	}

#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(transform.position, colliderSize);
	}
#endif
}
