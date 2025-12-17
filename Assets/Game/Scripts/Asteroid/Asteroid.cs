using System;
using BulletBase;
using ScoreBase;
using UnityEngine;
using UnityEngine.Serialization;

namespace AsteroidBase
{
    public class Asteroid: MonoBehaviour
    {
        [SerializeField] private ScoreDataSO scoreData;
        
        private Score _score;
        
        public event Action<Vector2> Exploded;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<Bullet>(out Bullet bullet) == false)
            {
                return;
            }
            
            gameObject.SetActive(false);
            _score.Add(scoreData.Points);
            Exploded?.Invoke(transform.position);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            gameObject.SetActive(false);
            _score.Add(scoreData.Points);
            Exploded?.Invoke(transform.position);
        }

        public void Init(Score score)
        {
            _score = score;
            enabled = true;
        }
    }
}