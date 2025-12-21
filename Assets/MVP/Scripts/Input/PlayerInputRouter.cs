using System;
using Model;
using UnityEngine.InputSystem;

namespace Game
{
    public class PlayerInputRouter
    {
        private PlayerInput _input;
        private Ship _model;

        public PlayerInputRouter(Ship model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            _model = model;
            _input = new PlayerInput();
        }

        public void OnEnable()
        {
            _input.Enable();
            
            _input.Player.Acceleration.performed += OnAccelerationPerformed;
            _input.Player.Acceleration.canceled += OnAccelerationCancelled;
            _input.Player.Rotation.performed += OnRotationPerformed;
            _input.Player.Rotation.canceled += OnRotationCanceled;
        }

        public void OnDisable()
        {
            _input.Disable();
            
            _input.Player.Acceleration.performed -= OnAccelerationPerformed;
            _input.Player.Acceleration.canceled -= OnAccelerationCancelled;
            _input.Player.Rotation.performed -= OnRotationPerformed;
            _input.Player.Rotation.canceled -= OnRotationCanceled;
        }
        
        private void OnAccelerationPerformed(InputAction.CallbackContext context)
        {
            _model.Move();
        }

        private void OnAccelerationCancelled(InputAction.CallbackContext context)
        {
            _model.Stop();
        }

        private void OnRotationPerformed(InputAction.CallbackContext context)
        {
            _model.Rotate(context.ReadValue<float>());
        }

        private void OnRotationCanceled(InputAction.CallbackContext context)
        {
            _model.StopRotate();
        }
    }
}