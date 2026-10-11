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
        private float _sailOpennessInput;
        private float _sailAngleInput;

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

        public float SailOpennessInput
        {
            get => _sailOpennessInput;
            set => _sailOpennessInput = Mathf.Clamp(value, -1f, 1f);
        }

        public float SailAngleInput
        {
            get => _sailAngleInput;
            set => _sailAngleInput = Mathf.Clamp(value, -1f, 1f);
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
            SailOpenness += _sailOpennessInput * _settings.SailOpennessRate * deltaTime;
            SailAngle += _sailAngleInput * _settings.SailAngleRate * deltaTime;

            Vector2 forward = Forward;
            Vector2 right = Right;

            Vector2 sailNormal = HeadingToDirection(Heading + _sailAngle);
            Vector2 apparentWind = wind.Velocity - Velocity;
            Vector2 sailForce = ComputeSailForce(sailNormal, apparentWind);
            Vector2 hullDrag = ComputeHullDrag(forward, right);
            float turnRate = ComputeTurnRate(sailForce, right);

            Velocity += (sailForce + hullDrag) * deltaTime;
            Position += Velocity * deltaTime;
            Heading = Mathf.Repeat(Heading + turnRate * deltaTime, 360f);
        }

        private Vector2 ComputeSailForce(Vector2 sailNormal, Vector2 apparentWind)
        {
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

        private float ComputeTurnRate(Vector2 sailForce, Vector2 right)
        {
            // The sideways part of the sail force swings the bow towards the side the sail pushes to.
            float sailTurn = Vector2.Dot(sailForce, right) * _settings.SailTurnCoefficient;

            return Mathf.Clamp(sailTurn, -_settings.MaxTurnRate, _settings.MaxTurnRate);
        }
    }
}
