using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Input", menuName = "Toolbox/Managers/Input Manager")]
public class InputManager : ManagerBase, IExecute
{
	//Create input actions assets named "Controls"
	private Controls controls;

	public Vector2 PointerPosition { get; private set; }
	public Vector2 PointerDelta { get; private set; }
	public Vector2 MoveInput { get; private set; }

	public event Action OnClick;

	public Controls GetBindings()
	{
		return controls;
	}

	private void OnEnable()
	{
		controls?.Enable();
	}

	private void OnDisable()
	{
		controls?.Disable();
	}

	private void OnDestroy()
	{
		controls.Dispose();
	}

	private void InitializeControls()
	{
		controls.Player.PointerPosition.performed += ctx => PointerPosition = ctx.ReadValue<Vector2>();
		controls.Player.PointerDelta.performed += ctx => PointerDelta = ctx.ReadValue<Vector2>();

		controls.Player.Movement.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
		controls.Player.Click.performed += _ => OnClick.Invoke();

		controls.Enable();
	}

	public void OnExecute()
	{
		controls?.Dispose();
		controls = new Controls();

		InitializeControls();
	}
}
