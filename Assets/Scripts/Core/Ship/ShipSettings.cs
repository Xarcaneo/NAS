using System;
using UnityEngine;

namespace Sailing.Core
{
    [Serializable]
    public class ShipSettings
    {
        [SerializeField] private float _sailForceCoefficient = 0.05f;
        [SerializeField] private float _abackForceFactor = 0.05f;
        [SerializeField] private float _forwardDragCoefficient = 0.02f;
        [SerializeField] private float _asternDragMultiplier = 4f;
        [SerializeField] private float _lateralDragCoefficient = 1.0f;
        [SerializeField] private float _maxTurnRate = 25f;
        [SerializeField] private float _sailOpennessRate = 50f;
        [SerializeField] private float _sailAngleRate = 45f;
        [SerializeField] private float _sailTurnCoefficient = 8f;

        public float SailForceCoefficient => _sailForceCoefficient;

        public float AbackForceFactor => _abackForceFactor;

        public float ForwardDragCoefficient => _forwardDragCoefficient;

        public float AsternDragMultiplier => _asternDragMultiplier;

        public float LateralDragCoefficient => _lateralDragCoefficient;

        public float MaxTurnRate => _maxTurnRate;

        public float SailOpennessRate => _sailOpennessRate;

        public float SailAngleRate => _sailAngleRate;

        public float SailTurnCoefficient => _sailTurnCoefficient;
    }
}
