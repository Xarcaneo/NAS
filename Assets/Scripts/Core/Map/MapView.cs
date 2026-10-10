using UnityEngine;

namespace Sailing.Core
{
    public class MapView : MonoBehaviour
    {
        [SerializeField] private ShipView _shipView;

        private Map _map;

        public void Initialize(Map map)
        {
            _map = map;
            _shipView.Initialize(_map.Ship);
        }
    }
}
