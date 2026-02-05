using System.Collections.Generic;
using UnityEngine;

namespace Obstacle
{
    public class AsteroidSpawnerData
    {
        public int PoolCount { get; private set; } = 10;
        public float Delay { get; private set; } = 4;
        public float MinPositionX { get; private set; } = 1;
        public float MaxPositionX { get; private set; } = 9;
        public float MaxPositionY { get; private set; } = 12;
        public float MinPositionY { get; private set; } = -1;
        public float Speed { get; private set; } = 2;
        public uint Points { get; private set; } = 50;

        private Queue<AsteroidData> _queue = new Queue<AsteroidData>();
        private float _currentPositionY;
        private float _currentDirectionY;

        public AsteroidSpawnerData()
        {
            for (int i = 0; i < PoolCount; i++)
            {
                AsteroidData asteroidData = new AsteroidData();
                _queue.Enqueue(asteroidData);
            }

            _currentPositionY = MaxPositionY;
        }

        public AsteroidData Spawn()
        {
            AsteroidData asteroidData = _queue.Dequeue();
            asteroidData.Transform.ChangePosition(CalculateRandomPosition());
            asteroidData.ChangeMoveDirection(CalculateRandomDirection());
            _queue.Enqueue(asteroidData);
            return asteroidData;
        }

        private Vector2 CalculateRandomPosition()
        {
            float randomXPosition = Random.Range(MinPositionX, MaxPositionX);
            Vector2 randomPosition = new Vector2(randomXPosition, _currentPositionY);

            if (Mathf.Approximately(_currentPositionY, MaxPositionY))
            {
                _currentPositionY = MinPositionY;
                _currentDirectionY = Vector2.down.y;
            }
            else
            {
                _currentPositionY = MaxPositionY;
                _currentDirectionY = Vector2.up.y;
            }

            return randomPosition;
        }

        private Vector2 CalculateRandomDirection()
        {
            float randomXDirection = Random.Range(Vector2.left.x, Vector2.right.x);
            Vector2 randomDirection = new Vector2(randomXDirection, _currentDirectionY);
            return randomDirection;
        }
    }
}