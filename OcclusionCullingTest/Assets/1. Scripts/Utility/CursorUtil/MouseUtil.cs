//#define NewInput //NOTE: Define when needed

using System;
using UnityEngine;
using VDFramework.Singleton;

#if NewInput
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

#else
using System.Linq;
#endif

namespace Utility.CursorUtil
{
	[DisallowMultipleComponent]
	public class MouseUtil : Singleton<MouseUtil>
	{
		private static event Action mouseButtonDown = delegate { };

		public static event Action MouseButtonDown
		{
			add
			{
				if (!IsInitialized)
				{
					Debug.LogWarning("Make sure the MouseUtil is in the scene!");
					return;
				}

				mouseButtonDown += value;
			}
			remove => mouseButtonDown -= value;
		}

		private static event Action mouseButtonUp = delegate { };

		public static event Action MouseButtonUp
		{
			add
			{
				if (!IsInitialized)
				{
					Debug.LogWarning("Make sure the MouseUtil is in the scene!");
					return;
				}

				mouseButtonUp += value;
			}
			remove => mouseButtonUp -= value;
		}

		public static bool IsMouseButtonDown { get; private set; }

		public static Vector3 MousePosition => GetMousePosition();

		private static void InvokeButtonDown()
		{
			mouseButtonDown.Invoke();
			IsMouseButtonDown = true;
		}

		private static void InvokeButtonUp()
		{
			mouseButtonUp.Invoke();
			IsMouseButtonDown = false;
		}

#if NewInput
		[SerializeField]
		private InputActionReference mouseClick;

		private void OnEnable()
		{
			mouseClick.action.Enable();
			mouseClick.action.performed += InvokeButtonDown;
			mouseClick.action.canceled += InvokeButtonUp;
		}

		private void OnDisable()
		{
			mouseClick.action.Disable();
			mouseClick.action.performed -= InvokeButtonDown;
			mouseClick.action.canceled -= InvokeButtonUp;
		}

		private static void InvokeButtonDown(InputAction.CallbackContext callback)
		{
			InvokeButtonDown();
		}

		private static void InvokeButtonUp(InputAction.CallbackContext callback)
		{
			InvokeButtonUp();
		}

		private static Vector3 GetMousePosition()
		{
			Vector2Control currentPosition = Mouse.current.position;

			return new Vector3(currentPosition.x.ReadValue(), currentPosition.y.ReadValue(), 0);
		}
#else
		[SerializeField]
		private int[] mouseButtonsToCheck = { 0, 1 }; // Left and Right mouse button

		private void Update()
		{
			if (!IsMouseButtonDown && mouseButtonsToCheck.Any(Input.GetMouseButtonDown))
			{
				InvokeButtonDown();
			}

			if (IsMouseButtonDown && mouseButtonsToCheck.Any(Input.GetMouseButtonUp))
			{
				InvokeButtonUp();
			}
		}

		private static Vector3 GetMousePosition()
		{
			return Input.mousePosition;
		}
#endif
	}
}