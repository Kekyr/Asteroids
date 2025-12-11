using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerBase
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float _force;
        [SerializeField] private float _torqueForce;

        [SerializeField] private float _minXPosition;
        [SerializeField] private float _maxXPosition;

        [SerializeField] private float _minYPosition;
        [SerializeField] private float _maxYPosition;

        private PlayerInput _playerInput;
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
            CheckPosition();
        }

        public void Init(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            enabled = true;
        }

        private void CheckPosition()
        {
            Vector2 checkedPosition = transform.position;

            checkedPosition.x = CheckValue(checkedPosition.x, _minXPosition, _maxXPosition);
            checkedPosition.y = CheckValue(checkedPosition.y, _minYPosition, _maxYPosition);

            transform.position = checkedPosition;
        }

        private float CheckValue(float value, float minValue, float maxValue)
        {
            if (value > maxValue)
            {
                return minValue;
            }

            if (value < minValue)
            {
                return maxValue;
            }

            return value;
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