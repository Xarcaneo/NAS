using UnityEngine;

namespace Sailing.Core
{
    public class MapView : MonoBehaviour
    {
        [SerializeField] private ShipView _shipView;

        public void Initialize(Map map)
        {
            _shipView.Initialize(map.Ship);
        }
    }
}
