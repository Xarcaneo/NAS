using UnityEngine;

namespace Sailing.Core
{
    public class Ship
    {
        public const float MinSailOpenness = 0f;
        public const float MaxSailOpenness = 100f;
        public const float MaxSailAngle = 45f;

        private ShipSettings _settings;
        private float _sailOpenness = MaxSailOpenness;
        private float _sailAngle;
        private float _rudder;

        public Vector2 Position { get; private set; }

        public float Heading { get; private set; }

        public Vector2 Velocity { get; private set; }

        public Vector2 Forward => HeadingToDirection(Heading);

        public Vector2 Right
        {
            get
            {
                Vector2 forward = Forward;
                return new Vector2(forward.y, -forward.x);
            }
        }

        public float ForwardSpeed => Vector2.Dot(Velocity, Forward);

        public float SailOpenness
        {
            get => _sailOpenness;
            set => _sailOpenness = Mathf.Clamp(value, MinSailOpenness, MaxSailOpenness);
        }

        public float SailAngle
        {
            get => _sailAngle;
            set => _sailAngle = Mathf.Clamp(value, -MaxSailAngle, MaxSailAngle);
        }

        public float Rudder
        {
            get => _rudder;
            set => _rudder = Mathf.Clamp(value, -1f, 1f);
        }

        public static Vector2 HeadingToDirection(float heading)
        {
            float radians = heading * Mathf.Deg2Rad;
            return new Vector2(Mathf.Sin(radians), Mathf.Cos(radians));
        }

        public void Configure(ShipSettings settings)
        {
            _settings = settings;
        }

        public void Place(Vector2 position, float heading)
        {
            Position = position;
            Heading = heading;
            Velocity = Vector2.zero;
        }

        public void Update(float deltaTime, Wind wind)
        {
            Vector2 forward = Forward;
            Vector2 right = Right;

            Vector2 acceleration = ComputeSailForce(wind) + ComputeHullDrag(forward, right);
            Velocity += acceleration * deltaTime;
            Position += Velocity * deltaTime;

            Heading = Mathf.Repeat(Heading + ComputeTurnRate() * deltaTime, 360f);
        }

        private Vector2 ComputeSailForce(Wind wind)
        {
            Vector2 sailNormal = HeadingToDirection(Heading + _sailAngle);
            Vector2 apparentWind = wind.Velocity - Velocity;

            float normalWind = Vector2.Dot(apparentWind, sailNormal);
            float pressure = normalWind * Mathf.Abs(normalWind) * _settings.SailForceCoefficient;
            float openness = _sailOpenness / MaxSailOpenness;

            // Wind hitting the front of the sail (aback) only produces weak sternway.
            if (normalWind < 0f)
                pressure *= _settings.AbackForceFactor;

            return sailNormal * (pressure * openness);
        }

        private Vector2 ComputeHullDrag(Vector2 forward, Vector2 right)
        {
            float forwardSpeed = Vector2.Dot(Velocity, forward);
            float lateralSpeed = Vector2.Dot(Velocity, right);

            float forwardDragCoefficient = _settings.ForwardDragCoefficient;
            if (forwardSpeed < 0f)
                forwardDragCoefficient *= _settings.AsternDragMultiplier;

            Vector2 forwardDrag = forward * (-forwardSpeed * Mathf.Abs(forwardSpeed) * forwardDragCoefficient);
            Vector2 lateralDrag = right * (-lateralSpeed * Mathf.Abs(lateralSpeed) * _settings.LateralDragCoefficient);

            return forwardDrag + lateralDrag;
        }

        private float ComputeTurnRate()
        {
            float effectiveness = Mathf.Clamp01(Mathf.Abs(ForwardSpeed) / _settings.RudderReferenceSpeed);

            return _rudder * _settings.MaxTurnRate * effectiveness;
        }
    }
}
