using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour, IAwake, ITick
{
	[SerializeField] private float speedMultiplier;
	public bool Process => gameObject.activeSelf;

	private List<List<Transform>> backgrounds = new List<List<Transform>>();
	Camera camera;

	public void OnAwake()
	{
		camera = Camera.main;

		Toolbox.GetManager<UpdateManager>().Add(this);

		for (int i = 0; i < transform.childCount; i++)
		{
			List<Transform> layer = new List<Transform>();

			layer.Add(transform.GetChild(i));

			Transform up = Instantiate(transform.GetChild(i));
			Transform down = Instantiate(transform.GetChild(i));

			up.SetParent(transform);
			down.SetParent(transform);
			
			up.position = camera.ViewportToWorldPoint(Vector3.up * 2);
			down.position = camera.ViewportToWorldPoint(Vector3.down);

			layer.Add(up);
			layer.Add(down);

			backgrounds.Add(layer);

		}
	}

	public void OnTick()
	{
		foreach (var layer in backgrounds)
		{
			foreach (var back in layer)
			{

				float yPos = camera.WorldToViewportPoint(back.position).y;

				if (yPos < -1)
				{
					back.position = camera.ViewportToWorldPoint(Vector3.up * 2);
				}
				else
				{
					if (back.position.z == 0) continue;
					back.position += Vector3.down * (speedMultiplier / back.position.z) * Time.deltaTime;
				}
			}
		}
	}
}
