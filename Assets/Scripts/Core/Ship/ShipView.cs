using UnityEngine;

namespace Sailing.Core
{
    public class ShipView : MonoBehaviour
    {
        [SerializeField] private ShipSettings _settings = new();
        // Angle between the mesh's forward axis and its bow. This model's bow faces -Z, so it uses 180.
        [SerializeField] private float _meshYawOffset;

        // The model has several sails in one mesh, so they all turn together.
        [SerializeField] private Transform _sail;

        private Ship _ship;
        private float _waterLevel;
        private Quaternion _sailRestRotation;

        public void Initialize(Ship ship)
        {
            _ship = ship;
            _waterLevel = transform.position.y;
            _sailRestRotation = _sail.localRotation;

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

            _sail.localRotation = _sailRestRotation * Quaternion.Euler(0f, _ship.SailAngle, 0f);
        }
    }
}
