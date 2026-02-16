using System.Collections.Generic;
using UnityEngine;

namespace Obstacle
{
    public class AsteroidFragmentSpawnerData
    {
        private float _positionXOffset = 1.5f;
        private float _positionYOffset= 0.4f;
        
        private Queue<AsteroidData> _queue = new Queue<AsteroidData>();
        
        public int PoolCount { get;} = 20;
        public int ExplodeCount { get;} = 2;
        public float Speed { get;} = 4;
        public uint Points { get;} = 100;

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
            float randomXPosition = Random.Range(position.x - _positionXOffset, position.x + _positionXOffset);
            float randomYPosition = Random.Range(position.y - _positionYOffset, position.y + _positionYOffset);
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