using System;
using Game;
using R3;
using UnityEngine;

namespace Player
{
    public class ShipData
    {
        public ReactiveProperty<Vector2> Position { get; } = new ReactiveProperty<Vector2>();
        public ReactiveProperty<float> Rotation { get; } = new ReactiveProperty<float>();
        public ReactiveProperty<Vector2> Velocity { get; } = new ReactiveProperty<Vector2>();

        public float MoveForce { get; } = 2f;
        public float RotationForce { get; } = 2f;
        public float RotationDirection { get; private set; }

        public bool IsMoving { get; private set; }
        public bool IsRotating { get; private set; }

        public void ChangePosition(Vector2 newPosition)
        {
            Position.Value = newPosition;
        }

        public void ChangeRotation(float newRotation)
        {
            Rotation.Value = newRotation;
        }
        
        public void Move()
        {
            IsMoving = true;
        }

        public void Stop()
        {
            IsMoving = false;
        }

        public void Rotate(float direction)
        {
            if (direction == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(direction));
            }

            if (Mathf.Approximately(RotationDirection, direction) == false)
            {
                RotationDirection = direction;
            }

            IsRotating = true;
        }

        public void StopRotate()
        {
            IsRotating = false;
        }

        public void ChangeVelocity(Vector2 newVelocity)
        {
            Velocity.Value = newVelocity;
        }
    }
}