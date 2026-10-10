using UnityEngine;
using UnityEngine.InputSystem;

namespace Sailing.Core
{
    public class GameModel : MonoBehaviour
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
            Game.Update(Time.deltaTime);
        }

        private void OnDestroy()
        {
            Game.Dispose();
        }

        private void StartGame()
        {
            Game = new Game();
            Game.Initialize(_inputActions);

            _gameView.Initialize(Game);
        }
    }
}
