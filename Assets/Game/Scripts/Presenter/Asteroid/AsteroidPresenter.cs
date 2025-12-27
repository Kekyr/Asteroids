using System;
using UnityEngine;
using Game;
using Model;

namespace Presenter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class AsteroidPresenter : MonoBehaviour
    {
        private Asteroid _model;
        private Rigidbody2D _rigidbody;
        private Helper _helper;

        private float _speed;
        
        public event Action<Vector2> Exploded;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        
        private void FixedUpdate()
        {
            _rigidbody.linearVelocity = _model.MoveDirection * _speed;
            transform.position = _helper.ClampPosition(transform.position);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<BulletPresenter>(out BulletPresenter bullet) == false)
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

        public void Init(Asteroid model)
        {
            _model = model;
            transform.position = model.Transform.Position;
            enabled = true;
        }
    }
}
