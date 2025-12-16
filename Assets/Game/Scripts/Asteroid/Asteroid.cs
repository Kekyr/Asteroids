using System;
using BulletBase;
using UnityEngine;

namespace AsteroidBase
{
    public class Asteroid: MonoBehaviour
    {
        public event Action<Vector2> Exploded;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<Bullet>(out Bullet bullet) == false)
            {
                return;
            }
            
            gameObject.SetActive(false);
            Exploded?.Invoke(transform.position);
        }
    }
}