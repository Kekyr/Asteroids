using System;
using System.Collections.Generic;

namespace Player
{
    public class GunData
    {
        public int PoolCount { get; private set; } = 15;
        public float BulletSpeed { get; private set; } = 8;

        private Queue<BulletData> _queue = new Queue<BulletData>();

        public event Action<BulletData> Shot;
        
        public GunData()
        {
            for (int i = 0; i < PoolCount; i++)
            {
                BulletData bulletData = new BulletData(BulletSpeed);
                _queue.Enqueue(bulletData);
            }
        }

        public void Shoot()
        {
            BulletData bulletData = _queue.Dequeue();
            Shot?.Invoke(bulletData);
            _queue.Enqueue(bulletData);
        }
    }
}