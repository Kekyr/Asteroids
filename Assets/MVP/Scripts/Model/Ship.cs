using System;
using UnityEngine;

namespace Model
{
    public class Ship
    {
        public Vector2 Position { get; private set; }
        public float Rotation { get; private set; }
        public Vector2 Velocity { get; private set; }
        
        public float MoveForce { get; private set; } = 2f;
        public float RotationForce { get; private set; } = 2f;
        public float RotationDirection { get; private set; }
        
        public bool IsMoving { get; private set; }
        public bool IsRotating { get; private set; }

        public event Action<Vector2> PositionChanged;
        public event Action<float> RotationChanged;
        public event Action<Vector2> VelocityChanged;
        
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

        public void ChangePosition(Vector2 newPosition)
        {
            Position = newPosition;
            PositionChanged?.Invoke(Position);
        }

        public void ChangeRotation(float newRotation)
        {
            Rotation = newRotation;
            RotationChanged?.Invoke(Rotation);
        }

        public void ChangeVelocity(Vector2 newVelocity)
        {
            Velocity = newVelocity;
            VelocityChanged?.Invoke(Velocity);
        }
    }
}