using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sailing.Core
{
    public sealed class InputManager : IDisposable
    {
        public const string MapName = "Game";

        private readonly List<ButtonBinding> _buttons = new();
        private InputActionAsset _actions;
        private InputActionMap _map;
        private InputAction _moveAction;

        public Vector2 MovementInput { get; private set; }

        public event Action<Vector2> MovementChanged;

        public ButtonBinding Q { get; private set; }
        public ButtonBinding W { get; private set; }
        public ButtonBinding E { get; private set; }

        public void Initialize(InputActionAsset actions)
        {
            _actions = actions;
            if (_actions == null)
            {
                Debug.LogError("[InputManager] No InputActionAsset: assign the input actions on the GameModel.");
                return;
            }

            _map = _actions.FindActionMap(MapName);
            if (_map == null)
            {
                Debug.LogError($"[InputManager] The input actions have no '{MapName}' map.");
                return;
            }

            _moveAction = _map.FindAction("Move");
            if (_moveAction != null)
            {
                _moveAction.performed += OnMovePerformed;
                _moveAction.canceled += OnMoveCanceled;
            }

            Q = CreateButton("Q");
            W = CreateButton("W");
            E = CreateButton("E");

            _map.Enable();
        }

        public void Dispose()
        {
            if (_moveAction != null)
            {
                _moveAction.performed -= OnMovePerformed;
                _moveAction.canceled -= OnMoveCanceled;
                _moveAction = null;
            }

            foreach (var button in _buttons)
                button.Dispose();

            _buttons.Clear();

            MovementInput = Vector2.zero;
            _map?.Disable();
            _map = null;
            _actions = null;
        }

        private ButtonBinding CreateButton(string actionName)
        {
            var action = _map.FindAction(actionName);
            if (action == null)
                Debug.LogWarning($"[InputManager] The '{MapName}' map has no '{actionName}' action.");

            var binding = new ButtonBinding(action);
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
