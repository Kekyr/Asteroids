using System;
using System.Collections.Generic;

namespace Model
{
    public class Gun
    {
        public int PoolCount { get; private set; } = 15;
        public float BulletSpeed { get; private set; } = 8;

        private Queue<Bullet> _queue = new Queue<Bullet>();

        public event Action<Bullet> Shot;
        
        public Gun()
        {
            for (int i = 0; i < PoolCount; i++)
            {
                Bullet bullet = new Bullet(BulletSpeed);
                _queue.Enqueue(bullet);
            }
        }

        public void Shoot()
        {
            Bullet bullet = _queue.Dequeue();
            Shot?.Invoke(bullet);
            _queue.Enqueue(bullet);
        }
    }
}