using Game;
using UnityEngine;

namespace AsteroidBase
{
    public class AsteroidMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Rigidbody2D _rigidbody;
        private Vector2 _moveDirection;
        private Helper _helper;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody.linearVelocity = _moveDirection * _speed;
            transform.position = _helper.ClampPosition(transform.position);
        }

        public void Init(Helper helper)
        {
            _helper = helper;
        }

        public void Init(Vector2 moveDirection)
        {
            _moveDirection = moveDirection;
        }
    }
}