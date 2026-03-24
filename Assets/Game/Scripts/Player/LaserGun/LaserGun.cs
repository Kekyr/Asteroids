using R3;
using UnityEngine;
using Zenject;

namespace Player
{
    public class LaserGun : ITickable
    {
        private GameObject _laser;
        private LaserGunData _data;

        private float _timeLeft;
        private float _restoreTimeLeft;

        private bool _isReady = true;
        private bool _isRestoring;

        public ReactiveProperty<int> ShootCount { get; }
        public ReactiveProperty<float> CoolDownTime { get; } = new ReactiveProperty<float>();

        public LaserGun(LaserGunData data, Ship ship)
        {
            _data = data;
            _laser = ship.Laser;
            ShootCount = new ReactiveProperty<int>(_data.MaxShootCount);
        }

        void ITickable.Tick()
        {
            Shoot();
            CoolDown();
            Restore();
        }

        public void TryToShoot()
        {
            if (ShootCount.Value != 0 && _isReady == true && _laser.activeSelf == false)
            {
                _laser.SetActive(true);
                _timeLeft = _data.ShootDuration;
                _restoreTimeLeft = _data.RestoreDuration;
            }
        }

        private void Shoot()
        {
            if (_laser.activeSelf == false)
            {
                return;
            }

            _timeLeft -= Time.deltaTime;

            if (_timeLeft <= 0)
            {
                _laser.SetActive(false);
                _isReady = false;
                _isRestoring = true;
                ShootCount.Value--;
                _timeLeft = _data.CoolDownDuration;
            }
        }

        private void CoolDown()
        {
            if (_isReady == true)
            {
                return;
            }

            _timeLeft -= Time.deltaTime;
            CoolDownTime.Value = _timeLeft;

            if (_timeLeft <= 0)
            {
                _isReady = true;
            }
        }

        private void Restore()
        {
            if (_isRestoring == false)
            {
                return;
            }

            _restoreTimeLeft -= Time.deltaTime;

            if (_restoreTimeLeft <= 0)
            {
                ShootCount.Value++;
                _restoreTimeLeft = _data.RestoreDuration;
            }

            if (ShootCount.Value == _data.MaxShootCount)
            {
                _isRestoring = false;
            }
        }
    }
}