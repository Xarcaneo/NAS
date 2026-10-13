using System;
using UnityEngine.InputSystem;

namespace Sailing.Core
{
    public class Game : IDisposable
    {
        public InputManager Input { get; private set; }

        public Map Map { get; private set; }

        public void Initialize(InputActionAsset actions)
        {
            Input = new InputManager();
            Input.Initialize(actions);

            Map = new Map();
        }

        public void Update(float deltaTime)
        {
            Ship ship = Map.Ship;
            ship.SailOpennessInput = Input.SailOpennessInput;
            ship.SailAngleInput = Input.SailRotationInput;

            Map.Update(deltaTime);
        }

        public void Dispose()
        {
            Input.Dispose();
        }
    }
}
