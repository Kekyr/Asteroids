using System;
using UnityEngine;

namespace Model
{
    public class Transformable
    {
        public Vector2 Position { get; private set; }
        public float Rotation { get; private set; }
        
        public event Action<Vector2> PositionChanged;
        public event Action<float> RotationChanged;
        
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
    }
}