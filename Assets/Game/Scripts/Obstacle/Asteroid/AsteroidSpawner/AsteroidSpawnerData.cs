using System.Collections.Generic;
using UnityEngine;

namespace Obstacle
{
    public class AsteroidSpawnerData
    {
        private float _minPositionX = 1;
        private float _maxPositionX = 9;
        private float _maxPositionY = 12;
        private float _minPositionY = -1;
        
        private Queue<AsteroidData> _queue = new Queue<AsteroidData>();
        private float _currentPositionY;
        private float _currentDirectionY;

        public int PoolCount { get; } = 10;
        public float Delay { get; } = 4;
        public float Speed { get; } = 2;
        public uint Points { get; } = 50;

        public AsteroidSpawnerData()
        {
            for (int i = 0; i < PoolCount; i++)
            {
                AsteroidData asteroidData = new AsteroidData();
                _queue.Enqueue(asteroidData);
            }

            _currentPositionY = _maxPositionY;
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
            float randomXPosition = Random.Range(_minPositionX, _maxPositionX);
            Vector2 randomPosition = new Vector2(randomXPosition, _currentPositionY);

            if (Mathf.Approximately(_currentPositionY, _maxPositionY))
            {
                _currentPositionY = _minPositionY;
                _currentDirectionY = Vector2.down.y;
            }
            else
            {
                _currentPositionY = _maxPositionY;
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