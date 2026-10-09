using System;
using UnityEngine.InputSystem;

namespace Sailing.Core
{
    public sealed class Game : IDisposable
    {
        private static Game s_instance;

        public static Game Instance => s_instance;

        public InputManager Input { get; private set; }

        public Game()
        {
            s_instance = this;
        }

        public void Initialize(InputActionAsset actions)
        {
            Input = new InputManager();
            Input.Initialize(actions);
        }

        public void Update()
        {
        }

        public void Dispose()
        {
            Input?.Dispose();
            Input = null;

            if (s_instance == this)
                s_instance = null;
        }
    }
}
