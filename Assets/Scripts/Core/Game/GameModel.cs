using UnityEngine;
using UnityEngine.InputSystem;

namespace Sailing.Core
{
    // Entry point in the scene. Creates the game and hands it to the views.
    public class GameModel : MonoBehaviour
    {
        [SerializeField] private GameView _gameView;
        [SerializeField] private InputActionAsset _inputActions;

        private Game _game;

        private void Awake()
        {
            StartGame();
        }

        private void Update()
        {
            _game.Update(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _game.Dispose();
        }

        private void StartGame()
        {
            _game = new Game();
            _game.Initialize(_inputActions);

            _gameView.Initialize(_game);
        }
    }
}
