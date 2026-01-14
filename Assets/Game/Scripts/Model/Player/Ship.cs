using System;
using UnityEngine;

namespace Model
{
    public class Ship
    {
        public Transformable Transform { get; private set; }
        public Vector2 Velocity { get; private set; }

        public float MoveForce { get; private set; } = 2f;
        public float RotationForce { get; private set; } = 2f;
        public float RotationDirection { get; private set; }

        public bool IsMoving { get; private set; }
        public bool IsRotating { get; private set; }
        
        public event Action<Vector2> VelocityChanged;

        public Ship()
        {
            Transform = new Transformable();
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
            Velocity = newVelocity;
            VelocityChanged?.Invoke(Velocity);
        }
    }
}