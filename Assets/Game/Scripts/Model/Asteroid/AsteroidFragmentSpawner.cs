using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class AsteroidFragmentSpawner
    {
        public int PoolCount { get; private set; } = 20;
        public int ExplodeCount { get; private set; } = 2;
        public float PositionXOffset { get; private set; } = 1.5f;
        public float PositionYOffset { get; private set; } = 0.4f;
        public float Speed { get; private set; } = 4;
        public uint Points { get; private set; } = 100;

        private Queue<Asteroid> _queue = new Queue<Asteroid>();

        public AsteroidFragmentSpawner()
        {
            for (int i = 0; i < PoolCount; i++)
            {
                Asteroid asteroid = new Asteroid();
                _queue.Enqueue(asteroid);
            }
        }

        public Asteroid Spawn(Vector2 position)
        {
            Asteroid asteroid = _queue.Dequeue();
            asteroid.Transform.ChangePosition(CalculateRandomPosition(position));
            asteroid.ChangeMoveDirection(CalculateRandomDirection());
            _queue.Enqueue(asteroid);
            return asteroid;
        }

        private Vector2 CalculateRandomPosition(Vector2 position)
        {
            float randomXPosition = Random.Range(position.x - PositionXOffset, position.x + PositionXOffset);
            float randomYPosition = Random.Range(position.y - PositionYOffset, position.y + PositionYOffset);
            Vector2 randomPosition = new Vector2(randomXPosition, randomYPosition);
            return randomPosition;
        }
        
        private Vector2 CalculateRandomDirection()
        {
            float randomXDirection = Random.Range(Vector2.left.x, Vector2.right.x);
            float randomYDirection = Random.Range(Vector2.down.y, Vector2.up.y);
            Vector2 randomDirection = new Vector2(randomXDirection, randomYDirection);
            return randomDirection;
        }
    }
}