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

        public Subject<Vector2> IsExploded { get; } = new Subject<Vector2>();

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
            IsExploded.OnNext(transform.position);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<Bullet>(out Bullet bullet) == false)
            {
                return;
            }

            gameObject.SetActive(false);
            IsExploded.OnNext(transform.position);
        }

        public void Construct(Helper helper, float speed)
        {
            _helper = helper;
            _speed = speed;
        }

        public void SetDirection(Vector2 moveDirection)
        {
            _moveDirection = moveDirection;
        }
    }
}