using UnityEngine;
using UnityEngine.UIElements;

namespace Sailing.Core
{
    [RequireComponent(typeof(UIDocument))]
    public class HudView : MonoBehaviour
    {
        private const string SailOpennessLabelName = "sail-openness-label";
        private const string SailRotationLabelName = "sail-rotation-label";

        private Ship _ship;
        private Label _sailOpennessLabel;
        private Label _sailRotationLabel;

        // Last values written to the labels, so the text is only rebuilt when the number changes.
        private int _shownSailOpenness = -1;
        private int _shownSailAngle = int.MinValue;

        public void Initialize(Ship ship)
        {
            _ship = ship;

            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            _sailOpennessLabel = root.Q<Label>(SailOpennessLabelName);
            _sailRotationLabel = root.Q<Label>(SailRotationLabelName);
        }

        private void Update()
        {
            int sailOpenness = Mathf.RoundToInt(_ship.SailOpenness);
            if (sailOpenness != _shownSailOpenness)
            {
                _shownSailOpenness = sailOpenness;
                _sailOpennessLabel.text = $"Sail openness: {sailOpenness}%";
            }

            int sailAngle = Mathf.RoundToInt(_ship.SailAngle);
            if (sailAngle != _shownSailAngle)
            {
                _shownSailAngle = sailAngle;
                _sailRotationLabel.text = FormatSailRotation(sailAngle);
            }
        }

        private static string FormatSailRotation(int sailAngle)
        {
            if (sailAngle == 0)
                return "Sail rotation: 0°";

            string side = sailAngle < 0 ? "left" : "right";
            return $"Sail rotation: {Mathf.Abs(sailAngle)}° {side}";
        }
    }
}
