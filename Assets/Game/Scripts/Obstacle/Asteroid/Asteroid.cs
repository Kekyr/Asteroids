using Game;
using Player;
using R3;
using UnityEngine;

namespace Obstacle
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private Helper _helper;

        private float _speed;
        private Vector2 _moveDirection;
        private bool _isOnScreen;

        public ReactiveProperty<Vector2> Exploded { get; } = new ReactiveProperty<Vector2>();

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody.linearVelocity = _moveDirection * _speed;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _moveDirection);

            if (_isOnScreen == false)
            {
                _isOnScreen = _helper.IsOnScreen(transform.position);
                return;
            }

            transform.position = _helper.ClampPosition(transform.position);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            gameObject.SetActive(false);
            Exploded.Value = transform.position;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<Bullet>(out Bullet bullet) == false)
            {
                return;
            }

            gameObject.SetActive(false);
            Exploded.Value = transform.position;
        }

        public void Init(Helper helper, float speed)
        {
            _helper = helper;
            _speed = speed;
        }

        public void Init(Vector2 moveDirection)
        {
            _moveDirection = moveDirection;
        }
    }
}