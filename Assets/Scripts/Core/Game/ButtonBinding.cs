using System;
using UnityEngine.InputSystem;

namespace Sailing.Core
{
    public sealed class ButtonBinding : IDisposable
    {
        private readonly InputAction _action;

        public event Action Pressed;
        public event Action Released;

        public bool IsHeld => _action != null && _action.IsPressed();

        public ButtonBinding(InputAction action)
        {
            _action = action;
            if (_action == null)
                return;

            _action.performed += OnPerformed;
            _action.canceled += OnCanceled;
        }

        public void Dispose()
        {
            if (_action == null)
                return;

            _action.performed -= OnPerformed;
            _action.canceled -= OnCanceled;
        }

        private void OnPerformed(InputAction.CallbackContext context) => Pressed?.Invoke();

        private void OnCanceled(InputAction.CallbackContext context) => Released?.Invoke();
    }
}
