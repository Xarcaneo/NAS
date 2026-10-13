using UnityEngine;

namespace Sailing.Core
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private MapView _mapView;
        [SerializeField] private HudView _hudView;

        public void Initialize(Game game)
        {
            _mapView.Initialize(game.Map);
            _hudView.Initialize(game.Map.Ship);
        }
    }
}
