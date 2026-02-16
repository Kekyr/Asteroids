using Game;
using UnityEngine;

namespace Obstacle
{
    public class AsteroidData
    {
        public Transformable Transform { get; private set; }
        public Vector2 MoveDirection { get; private set; }

        public AsteroidData()
        {
            Transform = new Transformable();
        }
        
        public void ChangeMoveDirection(Vector2 newDirection)
        {
            MoveDirection = newDirection;
        }
    }
}