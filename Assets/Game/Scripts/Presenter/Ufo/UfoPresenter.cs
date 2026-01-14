using System;
using UnityEngine;
using Game;
using Model;

namespace Presenter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class UfoPresenter : MonoBehaviour
    {
        private Ufo _model;

        private Rigidbody2D _rigidbody;
        private Transform _target;
        private Helper _helper;
        private SpriteRenderer _view;

        private bool _isOnScreen;

        public event Action Exploded;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _view = GetComponentInChildren<SpriteRenderer>();

            _rigidbody.centerOfMass = _view.transform.localPosition;
        }

        private void FixedUpdate()
        {
            if (_target.gameObject.activeSelf == true)
            {
                Vector2 direction = (_target.position - transform.position).normalized;
                _rigidbody.rotation = Quaternion.LookRotation(Vector3.forward, direction).eulerAngles.z;
                _rigidbody.linearVelocity = direction * _model.Speed;

                if (_isOnScreen == false)
                {
                    _isOnScreen = _helper.IsOnScreen(transform.position);
                    return;
                }

                transform.position = _helper.VerifyPosition(transform.position);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<BulletPresenter>(out BulletPresenter bullet) == false)
            {
                return;
            }

            Exploded?.Invoke();
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Exploded?.Invoke();
            gameObject.SetActive(false);
        }

        public void Init(Transform target, Helper helper)
        {
            _target = target;
            _helper = helper;
        }

        public void Init(Ufo model)
        {
            _model = model;
            transform.position = _model.Transform.Position;
            enabled = true;
        }
    }
}