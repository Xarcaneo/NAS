using UnityEngine;
using UnityEngine.InputSystem;

namespace Sailing.Core
{
    public sealed class GameModel : MonoBehaviour
    {
        [SerializeField] private GameView _gameView;
        [SerializeField] private InputActionAsset _inputActions;

        public Game Game { get; private set; }

        private void Awake()
        {
            StartGame();
        }

        private void Update()
        {
            Game?.Update();
        }

        private void OnDestroy()
        {
            Game?.Dispose();
            Game = null;
        }

        private void StartGame()
        {
            Game = new Game();
            Game.Initialize(_inputActions);

            if (_gameView != null)
                _gameView.Initialize(Game);
        }
    }
}
