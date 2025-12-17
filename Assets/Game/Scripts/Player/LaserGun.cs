using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerBase
{
    public class LaserGun : MonoBehaviour
    {
        [SerializeField] private GameObject _laser;
        [SerializeField] private int _maxShootCount;
        [SerializeField] private float _coolDownDuration;
        [SerializeField] private float _shootDuration;
        [SerializeField] private float _restoreDuration;

        private WaitForSeconds _waitCoolDown;
        private WaitForSeconds _waitShoot;
        private WaitForSeconds _waitRestore;

        private Coroutine _restoring;
        private PlayerInput _input;

        private int _shootCount;
        private float _coolDown;

        private bool _isReady = true;
        private bool _isShooting;
        private bool _isRestoring;

        public event Action<int> ShootCountChanged;
        public event Action<float> CoolDownChanged;

        private void Awake()
        {
            _waitCoolDown = new WaitForSeconds(_coolDownDuration);
            _waitShoot = new WaitForSeconds(_shootDuration);
            _waitRestore = new WaitForSeconds(_restoreDuration);
        }

        private void Start()
        {
            _shootCount = _maxShootCount;
            ShootCountChanged?.Invoke(_shootCount);
            
            _input.Player.LaserGun.performed += OnLaserGunPerformed;
        }

        private void Update()
        {
            if (_isReady == true)
            {
                return;
            }
            
            _coolDown -= Time.deltaTime;
            
            if (_coolDown <= 0)
            {
                _coolDown = 0f;
                _isReady = true;
            }
            
            float roundedCoolDown = (float)Math.Round(_coolDown, 1);
            CoolDownChanged?.Invoke(roundedCoolDown);
        }

        private void OnDestroy()
        {
            _input.Player.LaserGun.performed -= OnLaserGunPerformed;
        }

        public void Init(PlayerInput input)
        {
            _input = input;
            enabled = true;
        }

        private void OnLaserGunPerformed(InputAction.CallbackContext context)
        {
            if (_shootCount != 0 && _isReady == true && _isShooting == false)
            {
                StartCoroutine(Shoot());
            }
        }

        private IEnumerator Shoot()
        {
            _isShooting = true;
            _laser.gameObject.SetActive(true);

            yield return _waitShoot;

            _laser.gameObject.SetActive(false);
            
            _shootCount--;
            ShootCountChanged?.Invoke(_shootCount);

            LaunchCoolDown();
            LaunchRestore();

            _isShooting = false;
        }

        private void LaunchCoolDown()
        {
            _coolDown = _coolDownDuration;
            _isReady = false;
        }

        private void LaunchRestore()
        {
            if (_isRestoring == true)
            {
                return;
            }
            
            _isRestoring = true;
            _restoring = StartCoroutine(Restore());
        }

        private IEnumerator Restore()
        {
            while (_shootCount != _maxShootCount)
            {
                yield return _waitRestore;
                _shootCount++;
                ShootCountChanged?.Invoke(_shootCount);
            }

            _isRestoring = false;
        }
    }
}