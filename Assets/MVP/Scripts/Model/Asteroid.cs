using UnityEngine;

namespace Model
{
    public class Asteroid
    {
        public float Speed { get; private set; }
        public Vector2 MoveDirection { get; private set; }

        public void ChangeMoveDirection(Vector2 newDirection)
        {
            MoveDirection = newDirection;
        }
    }
}
