using Game;
using Model;
using UnityEngine;

namespace Presenter
{
    public class ShipPresenter : MonoBehaviour
    {
        private Ship _model;
        
        private Rigidbody2D _rigidbody;
        private Helper _helper;
        
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

        public void Init(Ship model, Helper helper)
        {
            _model = model;
            _helper = helper;
            enabled = true;
        }
    }
}