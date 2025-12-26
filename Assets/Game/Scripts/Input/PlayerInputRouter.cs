using System;
using UnityEngine.InputSystem;
using Model;

namespace Game
{
    public class PlayerInputRouter
    {
        private PlayerInput _input;
        private Ship _ship;
        private Gun _gun;
        private LaserGun _laserGun;

        public PlayerInputRouter(Ship ship, Gun gun, LaserGun laserGun)
        {
            if (ship == null)
            {
                throw new ArgumentNullException(nameof(ship));
            }
            
            if (gun == null)
            {
                throw new ArgumentNullException(nameof(gun));
            }

            if (laserGun == null)
            {
                throw new ArgumentNullException(nameof(laserGun));
            }

            _ship = ship;
            _gun = gun;
            _laserGun = laserGun;
            _input = new PlayerInput();
        }

        public void OnEnable()
        {
            _input.Enable();
            
            _input.Player.Acceleration.performed += OnAccelerationPerformed;
            _input.Player.Acceleration.canceled += OnAccelerationCancelled;
            _input.Player.Rotation.performed += OnRotationPerformed;
            _input.Player.Rotation.canceled += OnRotationCanceled;
            _input.Player.Gun.performed += OnGunPerformed;
            _input.Player.LaserGun.performed += OnLaserGunPerformed;
        }

        public void OnDisable()
        {
            _input.Disable();
            
            _input.Player.Acceleration.performed -= OnAccelerationPerformed;
            _input.Player.Acceleration.canceled -= OnAccelerationCancelled;
            _input.Player.Rotation.performed -= OnRotationPerformed;
            _input.Player.Rotation.canceled -= OnRotationCanceled;
            _input.Player.Gun.performed -= OnGunPerformed;
            _input.Player.LaserGun.performed -= OnLaserGunPerformed;
        }
        
        private void OnAccelerationPerformed(InputAction.CallbackContext context)
        {
            _ship.Move();
        }

        private void OnAccelerationCancelled(InputAction.CallbackContext context)
        {
            _ship.Stop();
        }

        private void OnRotationPerformed(InputAction.CallbackContext context)
        {
            _ship.Rotate(context.ReadValue<float>());
        }

        private void OnRotationCanceled(InputAction.CallbackContext context)
        {
            _ship.StopRotate();
        }

        private void OnGunPerformed(InputAction.CallbackContext context)
        {
            _gun.Shoot();
        }

        private void OnLaserGunPerformed(InputAction.CallbackContext context)
        {
            _laserGun.TryToShoot();
        }
    }
}