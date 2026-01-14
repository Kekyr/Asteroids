using System;
using UnityEngine;
using TMPro;
using Model;

namespace View
{
    public class ShipView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _positionTextMesh;
        [SerializeField] private TextMeshProUGUI _angleTextMesh;
        [SerializeField] private TextMeshProUGUI _velocityTextMesh;

        private Ship _model;

        private void Start()
        {
            _model.Transform.PositionChanged += OnPositionChanged;
            _model.Transform.RotationChanged += OnRotationChanged;
            _model.VelocityChanged += OnVelocityChanged;
        }

        private void OnDestroy()
        {
            _model.Transform.PositionChanged -= OnPositionChanged;
            _model.Transform.RotationChanged -= OnRotationChanged;
            _model.VelocityChanged -= OnVelocityChanged;
        }

        public void Init(Ship model)
        {
            _model = model;
            enabled = true;
        }

        private void OnPositionChanged(Vector2 position)
        {
            float roundedXPosition = (float)Math.Round(position.x, 1);
            float roundedYPosition = (float)Math.Round(position.y, 1);

            _positionTextMesh.text = $"Position X: {roundedXPosition}, Y: {roundedYPosition}";
        }

        private void OnVelocityChanged(Vector2 velocity)
        {
            float roundedXPosition = (float)Math.Round(velocity.x, 1);
            float roundedYPosition = (float)Math.Round(velocity.y, 1);

            _velocityTextMesh.text = $"Velocity X:{roundedXPosition}, Y: {roundedYPosition}";
        }

        private void OnRotationChanged(float angle)
        {
            int roundedAngle = (int)angle;
            _angleTextMesh.text = $"Angle: {roundedAngle}\u00b0";
        }
    }
}