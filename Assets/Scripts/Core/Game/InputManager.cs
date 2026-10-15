using System;
using UnityEngine.InputSystem;

namespace Sailing.Core
{
    public class InputManager : IDisposable
    {
        private const string MapName = "Game";

        private InputActionMap _map;
        private InputAction _sailOpennessAction;
        private InputAction _sailRotationAction;

        public float SailOpennessInput => _sailOpennessAction.ReadValue<float>();

        public float SailRotationInput => _sailRotationAction.ReadValue<float>();

        public void Initialize(InputActionAsset actions)
        {
            _map = actions.FindActionMap(MapName);
            _sailOpennessAction = _map.FindAction("SailOpenness");
            _sailRotationAction = _map.FindAction("SailRotation");

            _map.Enable();
        }

        public void Dispose()
        {
            _map.Disable();
        }
    }
}
