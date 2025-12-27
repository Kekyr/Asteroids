using UnityEngine;
using Game;
using Model;

namespace Presenter
{
    [RequireComponent(typeof(BulletPresenter))]
    public class BulletPresenter : MonoBehaviour
    {
        private Bullet _model;

        private Rigidbody2D _rigidbody;
        private Helper _helper;
        private Vector2 _direction;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody.linearVelocity = _direction * _model.Speed;

            if (_helper.CheckPosition(transform.position) == false)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            gameObject.SetActive(false);
        }

        public void Init(Bullet model, Vector2 direction)
        {
            _model = model;
            _direction = direction;
            enabled = true;
        }

        public void Init(Helper helper)
        {
            _helper = helper;
        }
    }
}