using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sailing.Core
{
    public class InputManager : IDisposable
    {
        public const string MapName = "Game";

        private readonly List<ButtonBinding> _buttons = new();
        private InputActionMap _map;
        private InputAction _moveAction;

        public Vector2 MovementInput { get; private set; }

        public event Action<Vector2> MovementChanged;

        public ButtonBinding Q { get; private set; }
        public ButtonBinding W { get; private set; }
        public ButtonBinding E { get; private set; }

        public void Initialize(InputActionAsset actions)
        {
            _map = actions.FindActionMap(MapName);

            _moveAction = _map.FindAction("Move");
            _moveAction.performed += OnMovePerformed;
            _moveAction.canceled += OnMoveCanceled;

            Q = CreateButton("Q");
            W = CreateButton("W");
            E = CreateButton("E");

            _map.Enable();
        }

        public void Dispose()
        {
            _moveAction.performed -= OnMovePerformed;
            _moveAction.canceled -= OnMoveCanceled;

            foreach (var button in _buttons)
                button.Dispose();

            _buttons.Clear();
            _map.Disable();
        }

        private ButtonBinding CreateButton(string actionName)
        {
            var binding = new ButtonBinding(_map.FindAction(actionName));
            _buttons.Add(binding);
            return binding;
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            MovementInput = context.ReadValue<Vector2>();
            MovementChanged?.Invoke(MovementInput);
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            MovementInput = Vector2.zero;
            MovementChanged?.Invoke(MovementInput);
        }
    }
}
