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
        
        public void Construct(Helper helper, float speed)
        {
            _speed = speed;
            _helper = helper;
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }
    }
}