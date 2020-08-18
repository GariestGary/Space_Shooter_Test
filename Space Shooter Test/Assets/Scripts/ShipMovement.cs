using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class ShipMovement : MonoBehaviour, IAwake, ITick
{
	[SerializeField] private float movementSpeed;
	[SerializeField] private float leftPadding;
	[SerializeField] private float rightPadding;
	[SerializeField] private float topPadding;
	[SerializeField] private float bottomPadding;

	private InputManager input => Toolbox.GetManager<InputManager>();
	private MessageManager msg => Toolbox.GetManager<MessageManager>();

	private Camera camera;

	public bool Process => gameObject.activeSelf;

	private Transform t;

	private bool isControlled;

	public void OnAwake()
	{
		camera = Camera.main;
		t = transform;
		Toolbox.GetManager<UpdateManager>().Add(this);
		isControlled = true;
		msg.Subscribe(ServiceShareData.LOOSE, OnLoose);
	}

	public void OnTick()
	{
		Move();
		ViewportClamp();
	}

	private void Move()
	{
		if (!isControlled) return;

		Vector3 direction = new Vector3(input.MoveInput.x, input.MoveInput.y, 0) * movementSpeed * Time.deltaTime;
		t.position += direction;
	}

	private void ViewportClamp()
	{
		Vector3 viewPos = camera.WorldToViewportPoint(t.position);

		viewPos = new Vector3(Mathf.Clamp(viewPos.x, 0 + leftPadding, 1 - rightPadding), Mathf.Clamp(viewPos.y, 0 + bottomPadding, 1 - topPadding), viewPos.z);

		t.position = camera.ViewportToWorldPoint(viewPos);
	}

	public void OnLoose()
	{
		isControlled = false;
	}

	public void OnWin()
	{
		isControlled = false;
	}
}
