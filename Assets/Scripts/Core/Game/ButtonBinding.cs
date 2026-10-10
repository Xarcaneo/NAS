using System;
using UnityEngine.InputSystem;

namespace Sailing.Core
{
    public class ButtonBinding : IDisposable
    {
        private readonly InputAction _action;

        public event Action Pressed;
        public event Action Released;

        public bool IsHeld => _action.IsPressed();

        public ButtonBinding(InputAction action)
        {
            _action = action;
            _action.performed += OnPerformed;
            _action.canceled += OnCanceled;
        }

        public void Dispose()
        {
            _action.performed -= OnPerformed;
            _action.canceled -= OnCanceled;
        }

        private void OnPerformed(InputAction.CallbackContext context) => Pressed?.Invoke();

        private void OnCanceled(InputAction.CallbackContext context) => Released?.Invoke();
    }
}
