using R3;
using UnityEngine;

namespace Game
{
    public class Transformable
    {
        public ReactiveProperty<Vector2> Position { get;}
        public ReactiveProperty<float> Rotation { get;}

        public Transformable()
        {
            Position = new ReactiveProperty<Vector2>();
            Rotation = new ReactiveProperty<float>();
        }

        public void ChangePosition(Vector2 newPosition)
        {
            Position.Value = newPosition;
        }

        public void ChangeRotation(float newRotation)
        {
            Rotation.Value = newRotation;
        }
    }
}