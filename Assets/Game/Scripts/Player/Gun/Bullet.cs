using Game;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Bullet))]
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private Helper _helper;
        
        private Vector2 _direction;
        private float _speed;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody.linearVelocity = _direction * _speed;

            if (_helper.CheckPosition(transform.position) == false)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            gameObject.SetActive(false);
        }

        public void Init(float speed, Vector2 direction)
        {
            _speed = speed;
            _direction = direction;
        }

        public void Init(Helper helper)
        {
            _helper = helper;
        }
    }
}