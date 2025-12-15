using Game;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerBase
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float _force;
        [SerializeField] private float _torqueForce;

        private PlayerInput _playerInput;
        private Helper _helper;
        private Rigidbody2D _rigidbody;

        private bool _canMove;
        private float _torqueDirection;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();

            _playerInput.Player.Acceleration.performed += OnAccelerationPerformed;
            _playerInput.Player.Acceleration.canceled += OnAccelerationCancelled;
            _playerInput.Player.Rotation.performed += OnRotationPerformed;
            _playerInput.Player.Rotation.canceled += OnRotationCanceled;
        }

        private void OnDestroy()
        {
            _playerInput.Player.Acceleration.performed -= OnAccelerationPerformed;
            _playerInput.Player.Acceleration.canceled -= OnAccelerationCancelled;
            _playerInput.Player.Rotation.performed -= OnRotationPerformed;
            _playerInput.Player.Rotation.canceled -= OnRotationCanceled;
        }

        private void FixedUpdate()
        {
            if (_canMove)
            {
                _rigidbody.AddForce(transform.up * _force);
            }

            _rigidbody.AddTorque(_torqueDirection * _torqueForce);

            transform.position = _helper.CheckPosition(transform.position);
        }

        public void Init(PlayerInput playerInput, Helper helper)
        {
            _playerInput = playerInput;
            _helper = helper;
            enabled = true;
        }

        private void OnAccelerationPerformed(InputAction.CallbackContext context)
        {
            _canMove = true;
        }

        private void OnAccelerationCancelled(InputAction.CallbackContext context)
        {
            _canMove = false;
        }

        private void OnRotationPerformed(InputAction.CallbackContext context)
        {
            _torqueDirection = context.ReadValue<float>();
        }

        private void OnRotationCanceled(InputAction.CallbackContext context)
        {
            _torqueDirection = 0;
        }
    }
}