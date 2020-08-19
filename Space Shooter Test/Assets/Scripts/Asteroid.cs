using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Asteroid : MonoBehaviour, IEnemy, IPooledObject, ITick, IAwake
{
	[SerializeField] private AudioClip explodeSound;
	[Space]
	[SerializeField] private float minSize;
	[SerializeField] private float maxSize;
	[Space]
	[SerializeField] private float minSpeed;
	[SerializeField] private float maxSpeed;
	[Space]
	[SerializeField] private float minHealth;
	[SerializeField] private float maxHealth;
	[Space]
	[SerializeField] private float minRotationSpeed;
	[SerializeField] private float maxRotationSpeed;

	private Transform t;
	private ParticleSystem particles;
	private SpriteRenderer sprite;
	private Collider2D collider;
	private float currentSpeed;
	private float currentRotationSpeed;
	private float currentSize;
	private Camera camera;

	public bool Process => gameObject.activeSelf;

	public int CurrentHealth => currentHealth;
	private int currentHealth;

	public void Hit()
	{
		currentHealth--;



		if(currentHealth <= 0)
		{
			Kill();
		}

		float size = Mathf.Lerp(minSize, currentSize, currentHealth / maxHealth);
		t.localScale = new Vector3(size, size, 1);
	}

	public void Kill()
	{
		Toolbox.GetManager<MessageManager>().Send(ServiceShareData.ASTEROID_DESTROYED);
		sprite.enabled = false;
		collider.enabled = false;

		AudioPlayer.Instance?.Play(explodeSound);

		particles.Play();

		ObjectPooler.Instance.Despawn(gameObject, 1);
	}

	public void OnAwake()
	{
		particles = GetComponent<ParticleSystem>();
		sprite = GetComponent<SpriteRenderer>();
		collider = GetComponent<Collider2D>();
		t = transform;
		camera = Camera.main;
		currentSpeed = minSpeed;
		Toolbox.GetManager<UpdateManager>().Add(this);
	}

	public void OnSpawn(object data)
	{
		sprite.enabled = true;
		collider.enabled = true;
		particles.Stop();
		currentSize = UnityEngine.Random.Range(minSize, maxSize);

		t.localScale = new Vector3(currentSize, currentSize, 1);

		float multiplier = currentSize / maxSize;

		currentSpeed = Mathf.Lerp(maxSpeed, minSpeed, multiplier * multiplier);
		currentHealth = (int)Mathf.Lerp(minHealth, maxHealth, multiplier * multiplier);
		currentRotationSpeed = Mathf.Lerp(maxRotationSpeed, minRotationSpeed, multiplier * multiplier);

		t.eulerAngles = new Vector3(0, 0, UnityEngine.Random.Range(0.0f, 359.0f));
	}

	public void OnTick()
	{
		Move();
		Rotate();
	}

	public void Rotate()
	{
		t.Rotate(t.forward, currentRotationSpeed * Time.deltaTime);
	}

	private void Move()
	{
		t.position += -Vector3.up * currentSpeed * Time.deltaTime;

		float yPos = camera.WorldToViewportPoint(t.position).y;

		if (yPos < -0.5f)
		{
			ObjectPooler.Instance.Despawn(gameObject);
		}
	}
}
