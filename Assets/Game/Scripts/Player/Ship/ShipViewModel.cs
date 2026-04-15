using System;
using Player;
using R3;
using UnityEngine;
using Zenject;

namespace ViewModel
{
    public class ShipViewModel : IInitializable, IDisposable
    {
        public readonly ReactiveProperty<string> Position;
        public readonly ReactiveProperty<string> Rotation;
        public readonly ReactiveProperty<string> Velocity;

        private ShipData _model;
        private CompositeDisposable _disposables;

        public ShipViewModel(ShipData model)
        {
            _model = model;
            Position = new ReactiveProperty<string>();
            Rotation = new ReactiveProperty<string>();
            Velocity = new ReactiveProperty<string>();
            _disposables = new CompositeDisposable();
        }

        public void Initialize()
        {
            _model.Position.Subscribe(OnPositionChanged).AddTo(_disposables);
            _model.Rotation.Subscribe(OnRotationChanged).AddTo(_disposables);
            _model.Velocity.Subscribe(OnVelocityChanged).AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        private void OnPositionChanged(Vector2 position)
        {
            float roundedXPosition = (float)Math.Round(position.x, 1);
            float roundedYPosition = (float)Math.Round(position.y, 1);

            Position.Value = $"Position X: {roundedXPosition}, Y: {roundedYPosition}";
        }

        private void OnRotationChanged(float angle)
        {
            int roundedAngle = (int)angle;
            Rotation.Value = $"Angle: {roundedAngle}\u00b0";
        }

        private void OnVelocityChanged(Vector2 velocity)
        {
            float roundedXPosition = (float)Math.Round(velocity.x, 1);
            float roundedYPosition = (float)Math.Round(velocity.y, 1);

            Velocity.Value = $"Velocity X:{roundedXPosition}, Y: {roundedYPosition}";
        }
    }
}