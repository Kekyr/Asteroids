using System;
using UnityEngine;
using Game;
using Model;

namespace Presenter
{
    public class ShipPresenter : MonoBehaviour
    {
        private Ship _model;
        
        private Rigidbody2D _rigidbody;
        private Helper _helper;

        public event Action Exploded;
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
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
            
            _model.ChangePosition(transform.position);
            _model.ChangeRotation(transform.eulerAngles.z);
            _model.ChangeVelocity(_rigidbody.linearVelocity);
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            gameObject.SetActive(false);
            Exploded?.Invoke();
        }

        public void Init(Ship model, Helper helper)
        {
            _model = model;
            _helper = helper;
            enabled = true;
        }
    }
}