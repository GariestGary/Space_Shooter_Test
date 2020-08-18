using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Asteroid : MonoBehaviour, IEnemy, IPooledObject, ITick, IAwake
{
	[SerializeField] private float minSize;
	[SerializeField] private float maxSize;
	[SerializeField] private float minSpeed;
	[SerializeField] private float maxSpeed;

	private Transform t;
	private float currentSpeed;
	private Camera camera;
	private bool killed = false;

	public bool Process => gameObject.activeSelf;

	public void Kill()
	{
		if (killed) return;

		killed = true;
		Toolbox.GetManager<MessageManager>().Send(ServiceShareData.ASTEROID_DESTROYED);
		ObjectPooler.Instance.Despawn(gameObject, 0);
	}

	public void OnAwake()
	{
		t = transform;
		camera = Camera.main;
		currentSpeed = minSpeed;
		Toolbox.GetManager<UpdateManager>().Add(this);
	}

	public void OnSpawn(object data)
	{
		killed = false;
		float scale = UnityEngine.Random.Range(minSize, maxSize);
		t.localScale = new Vector3(scale, scale, 1);

		float tSpeed = scale / maxSize;

		currentSpeed = Mathf.Lerp(maxSpeed, minSpeed, tSpeed * tSpeed);
	}

	public void OnTick()
	{
		Move();
	}

	private void Move()
	{
		t.position += -t.up * currentSpeed * Time.deltaTime;

		float yPos = camera.WorldToViewportPoint(t.position).y;

		if (yPos < -1)
		{
			ObjectPooler.Instance.Despawn(gameObject);
		}
	}
}
