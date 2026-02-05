using System.Collections.Generic;
using UnityEngine;

namespace Obstacle
{
    public class AsteroidFragmentSpawnerData
    {
        public int PoolCount { get; private set; } = 20;
        public int ExplodeCount { get; private set; } = 2;
        public float PositionXOffset { get; private set; } = 1.5f;
        public float PositionYOffset { get; private set; } = 0.4f;
        public float Speed { get; private set; } = 4;
        public uint Points { get; private set; } = 100;

        private Queue<AsteroidData> _queue = new Queue<AsteroidData>();

        public AsteroidFragmentSpawnerData()
        {
            for (int i = 0; i < PoolCount; i++)
            {
                AsteroidData asteroidData = new AsteroidData();
                _queue.Enqueue(asteroidData);
            }
        }

        public AsteroidData Spawn(Vector2 position)
        {
            AsteroidData asteroidData = _queue.Dequeue();
            asteroidData.Transform.ChangePosition(CalculateRandomPosition(position));
            asteroidData.ChangeMoveDirection(CalculateRandomDirection());
            _queue.Enqueue(asteroidData);
            return asteroidData;
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