using R3;
using UnityEngine;

namespace Player
{
    public class LaserGun
    {
        private GameObject _laser;
        private LaserGunData _data;
        
        private float _timeLeft;
        private float _restoreTimeLeft;
        
        private bool _isReady = true;
        private bool _isRestoring;

        public ReactiveProperty<int> ShootCount { get; }
        public ReactiveProperty<float> CoolDownChanged { get; } = new ReactiveProperty<float>();

        public LaserGun(LaserGunData data, GameObject laser)
        {
            _data = data;
            _laser = laser;
            ShootCount = new ReactiveProperty<int>(_data.MaxShootCount);
        }

        public void Update(float timePassed)
        {
            Shoot(timePassed);
            CoolDown(timePassed);
            Restore(timePassed);
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

        private void Shoot(float timePassed)
        {
            if (_laser.activeSelf == false)
            {
                return;
            }

            _timeLeft -= timePassed;

            if (_timeLeft <= 0)
            {
                _laser.SetActive(false);
                _isReady = false;
                _isRestoring = true;
                ShootCount.Value--;
                _timeLeft = _data.CoolDownDuration;
            }
        }

        private void CoolDown(float timePassed)
        {
            if (_isReady == true)
            {
                return;
            }

            _timeLeft -= timePassed;
            CoolDownChanged.Value = _timeLeft;

            if (_timeLeft <= 0)
            {
                _isReady = true;
            }
        }

        private void Restore(float timePassed)
        {
            if (_isRestoring == false)
            {
                return;
            }

            _restoreTimeLeft -= timePassed;

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