using System;
using Game;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ship : MonoBehaviour
    {
        private ShipData _model;

        private Rigidbody2D _rigidbody;
        private Helper _helper;
        private SpriteRenderer _view;

        public event Action Exploded;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _view = GetComponentInChildren<SpriteRenderer>();
            
            _rigidbody.centerOfMass = _view.transform.localPosition;
        }

        private void FixedUpdate()
        {
            if (_model.IsMoving)
            {
                _rigidbody.AddForce(transform.up * _model.MoveForce);
            }

            if (_model.IsRotating)
            {
                _rigidbody.AddTorque(_model.RotationDirection * _model.RotationForce);
            }

            transform.position = _helper.ClampPosition(transform.position);

            _model.Transform.ChangePosition(transform.position);
            _model.Transform.ChangeRotation(transform.eulerAngles.z);
            _model.ChangeVelocity(_rigidbody.linearVelocity);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            gameObject.SetActive(false);
            Exploded?.Invoke();
        }

        public void Init(ShipData model, Helper helper)
        {
            _model = model;
            _helper = helper;
        }
    }
}