using System;
using Player;
using UnityEngine.InputSystem;

namespace Game
{
    public class PlayerInputRouter
    {
        private PlayerInput _input;
        private ShipData _shipData;
        private GunData _gunData;
        private LaserGunData _laserGunData;

        public PlayerInputRouter(ShipData shipData, GunData gunData, LaserGunData laserGunData)
        {
            if (shipData == null)
            {
                throw new ArgumentNullException(nameof(shipData));
            }
            
            if (gunData == null)
            {
                throw new ArgumentNullException(nameof(gunData));
            }

            if (laserGunData == null)
            {
                throw new ArgumentNullException(nameof(laserGunData));
            }

            _shipData = shipData;
            _gunData = gunData;
            _laserGunData = laserGunData;
            _input = new PlayerInput();
        }

        public void Start()
        {
            _input.Enable();
            
            _input.Player.Acceleration.performed += OnAccelerationPerformed;
            _input.Player.Acceleration.canceled += OnAccelerationCancelled;
            _input.Player.Rotation.performed += OnRotationPerformed;
            _input.Player.Rotation.canceled += OnRotationCanceled;
            _input.Player.Gun.performed += OnGunPerformed;
            _input.Player.LaserGun.performed += OnLaserGunPerformed;
        }

        public void OnDestroy()
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
            _shipData.Move();
        }

        private void OnAccelerationCancelled(InputAction.CallbackContext context)
        {
            _shipData.Stop();
        }

        private void OnRotationPerformed(InputAction.CallbackContext context)
        {
            _shipData.Rotate(context.ReadValue<float>());
        }

        private void OnRotationCanceled(InputAction.CallbackContext context)
        {
            _shipData.StopRotate();
        }

        private void OnGunPerformed(InputAction.CallbackContext context)
        {
            _gunData.Shoot();
        }

        private void OnLaserGunPerformed(InputAction.CallbackContext context)
        {
            _laserGunData.TryToShoot();
        }
    }
}