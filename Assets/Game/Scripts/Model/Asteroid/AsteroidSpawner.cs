using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class AsteroidSpawner
    {
        public int PoolCount { get; private set; } = 10;
        public float Delay { get; private set; } = 4;
        public float MinPositionX { get; private set; } = -1;
        public float MaxPositionX { get; private set; } = 11;
        public float PositionY { get; private set; } = 12;
        public float Speed { get; private set; } = 2;
        public uint Points { get; private set; } = 50;

        private Queue<Asteroid> _queue = new Queue<Asteroid>();

        public AsteroidSpawner()
        {
            for (int i = 0; i < PoolCount; i++)
            {
                Asteroid asteroid = new Asteroid();
                _queue.Enqueue(asteroid);
            }
        }

        public Asteroid Spawn()
        {
            Asteroid asteroid = _queue.Dequeue();
            asteroid.Transform.ChangePosition(CalculateRandomPosition());
            asteroid.ChangeMoveDirection(CalculateRandomDirection());
            _queue.Enqueue(asteroid);
            return asteroid;
        }

        private Vector2 CalculateRandomPosition()
        {
            float randomXPosition = Random.Range(MinPositionX, MaxPositionX);
            Vector2 randomPosition = new Vector2(randomXPosition, PositionY);
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