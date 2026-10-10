using UnityEngine;

namespace Sailing.Core
{
    public readonly struct Wind
    {
        public Wind(float heading, float speed)
        {
            Heading = heading;
            Speed = speed;
        }

        public float Heading { get; }

        public float Speed { get; }

        public Vector2 Velocity => Ship.HeadingToDirection(Heading) * Speed;
    }
}
