using UnityEngine;

namespace Sailing.Core
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private MapView _mapView;

        public void Initialize(Game game)
        {
            _mapView.Initialize(game.Map);
        }
    }
}
