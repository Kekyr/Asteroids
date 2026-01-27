using System;
using UnityEngine;
using TMPro;
using Model;
using R3;

namespace View
{
    public class ShipView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _positionTextMesh;
        [SerializeField] private TextMeshProUGUI _angleTextMesh;
        [SerializeField] private TextMeshProUGUI _velocityTextMesh;

        private Ship _model;

        private IDisposable _positionChanged;
        private IDisposable _rotationChanged;
        private IDisposable _velocityChanged;

        private void Start()
        {
            _positionChanged = _model.Transform.Position.Subscribe(OnPositionChanged);
            _rotationChanged = _model.Transform.Rotation.Subscribe(OnRotationChanged);
            _velocityChanged = _model.Velocity.Subscribe(OnVelocityChanged);
        }

        private void OnDestroy()
        {
            _positionChanged.Dispose();
            _rotationChanged.Dispose();
            _velocityChanged.Dispose();
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