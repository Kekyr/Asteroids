using BulletBase;
using ScoreBase;
using UnityEngine;

namespace UfoBase
{
    public class Ufo : MonoBehaviour
    {
        [SerializeField] private ScoreDataSO _scoreData;
        
        private Score _score;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<Bullet>(out Bullet bullet) == false)
            {
                return;
            }
            
            _score.Add(_scoreData.Points);
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _score.Add(_scoreData.Points);
            gameObject.SetActive(false);
        }

        public void Init(Score score)
        {
            _score = score;
            enabled = true;
        }
    }
}