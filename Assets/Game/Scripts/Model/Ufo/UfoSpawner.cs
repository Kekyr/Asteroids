using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class UfoSpawner
    {
        public int PoolCount { get; private set; } = 10;
        public float Speed { get; private set; } = 1;
        public float Delay { get; private set; } = 6;
        public float MinPositionY { get; private set; } = 0;
        public float MaxPositionY { get; private set; } = 12;
        public float PositionX { get; private set; } = 11;
        public uint Points { get; private set; } = 150;
        
        private Queue<Ufo> _queue = new Queue<Ufo>();

        public UfoSpawner()
        {
            for (int i = 0; i < PoolCount; i++)
            {
                Ufo ufo = new Ufo(Speed);
                _queue.Enqueue(ufo);
            }
        }

        public Ufo Spawn()
        {
            Ufo ufo = _queue.Dequeue();
            ufo.Transform.ChangePosition(CalculateRandomPosition());
            _queue.Enqueue(ufo);
            return ufo;
        }

        private Vector2 CalculateRandomPosition()
        {
            float randomYPosition = Random.Range(MinPositionY, MaxPositionY);
            Vector2 randomPosition = new Vector2(PositionX, randomYPosition);
            return randomPosition;
        }
    }
}