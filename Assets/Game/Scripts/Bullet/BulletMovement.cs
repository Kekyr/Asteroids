using UnityEngine;

namespace BulletBase
{
    public class BulletMovement : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private Vector2 _direction;
        private float _speed;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody.linearVelocity = _direction * _speed;
        }

        public void Init(Vector2 direction, float speed)
        {
            _direction = direction;
            _speed = speed;
        }
    }
}
