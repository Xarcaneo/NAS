using UnityEngine;

namespace Sailing.Core
{
    public class ShipView : MonoBehaviour
    {
        [SerializeField] private ShipSettings _settings = new();
        [SerializeField] private float _meshYawOffset;

        private Ship _ship;
        private float _waterLevel;

        public void Initialize(Ship ship)
        {
            _ship = ship;
            _waterLevel = transform.position.y;

            _ship.Configure(_settings);
            _ship.Place(
                new Vector2(transform.position.x, transform.position.z),
                transform.eulerAngles.y - _meshYawOffset);
        }

        private void LateUpdate()
        {
            transform.SetPositionAndRotation(
                new Vector3(_ship.Position.x, _waterLevel, _ship.Position.y),
                Quaternion.Euler(0f, _ship.Heading + _meshYawOffset, 0f));
        }
    }
}
