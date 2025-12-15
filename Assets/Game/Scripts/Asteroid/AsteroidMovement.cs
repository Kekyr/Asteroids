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
            transform.position = _helper.CheckPosition(transform.position);
        }

        public void Init(Helper helper,Vector2 moveDirection)
        {
            _helper = helper;
            _moveDirection = moveDirection;
        }
    }
}