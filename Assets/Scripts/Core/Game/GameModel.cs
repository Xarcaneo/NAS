using UnityEngine;

public sealed class GameModel : MonoBehaviour
{
    [SerializeField] private GameView gameView;

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
    }

    private void StartGame()
    {
        Game = new Game();
        Game.Initialize();

        if (gameView != null)
            gameView.Initialize(Game);
    }
}
