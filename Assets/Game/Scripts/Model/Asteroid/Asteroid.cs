using UnityEngine;

namespace Model
{
    public class Asteroid
    {
        public Transformable Transform { get; private set; }
        public Vector2 MoveDirection { get; private set; }

        public Asteroid()
        {
            Transform = new Transformable();
        }
        
        public void ChangeMoveDirection(Vector2 newDirection)
        {
            MoveDirection = newDirection;
        }
    }
}