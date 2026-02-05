using System;
using Game;
using Player;
using UnityEngine;

namespace Obstacle
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : MonoBehaviour
    {
        private AsteroidData _model;
        private Rigidbody2D _rigidbody;
        private Helper _helper;

        private float _speed;
        private bool _isOnScreen;
        
        public event Action<Vector2> Exploded;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        
        private void FixedUpdate()
        {
            _rigidbody.linearVelocity = _model.MoveDirection * _speed;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _model.MoveDirection);
            
            if (_isOnScreen == false)
            {
                _isOnScreen = _helper.IsOnScreen(transform.position);
                return;
            }
            
            transform.position = _helper.ClampPosition(transform.position);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<Bullet>(out Bullet bullet) == false)
            {
                return;
            }
            
            gameObject.SetActive(false);
            Exploded?.Invoke(transform.position);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            gameObject.SetActive(false);
            Exploded?.Invoke(transform.position);
        }

        public void Init(Helper helper, float speed)
        {
            _helper = helper;
            _speed = speed;
        }

        public void Init(AsteroidData model)
        {
            _model = model;
            transform.position = model.Transform.Position.Value;
        }
    }
}
