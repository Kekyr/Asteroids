using System;

namespace Model
{
    public class LaserGun
    {
        public int MaxShootCount { get; private set; } = 3;
        public float CoolDownDuration { get; private set; } = 2;
        public float ShootDuration { get; private set; } = 0.5f;
        public float RestoreDuration { get; private set; } = 4;

        private int _shootCount;
        private float _timeLeft;
        private float _restoreTimeLeft;

        private bool _isReady = true;
        private bool _isShooting;
        private bool _isRestoring;

        public event Action<int> ShootCountChanged;
        public event Action<float> CoolDownChanged;

        public event Action ShootStarted;
        public event Action ShootEnded;

        public void OnEnable()
        {
            _shootCount = MaxShootCount;
            ShootCountChanged?.Invoke(_shootCount);
        }

        public void Update(float timePassed)
        {
            Shoot(timePassed);
            CoolDown(timePassed);
            Restore(timePassed);
        }

        public void TryToShoot()
        {
            if (_shootCount != 0 && _isReady == true && _isShooting == false)
            {
                ShootStarted?.Invoke();
                _isShooting = true;
                _timeLeft = ShootDuration;
                _restoreTimeLeft = RestoreDuration;
            }
        }

        private void Shoot(float timePassed)
        {
            if (_isShooting == false)
            {
                return;
            }

            _timeLeft -= timePassed;

            if (_timeLeft <= 0)
            {
                ShootEnded?.Invoke();
                _isShooting = false;
                _isReady = false;
                _isRestoring = true;
                _shootCount--;
                ShootCountChanged?.Invoke(_shootCount);
                _timeLeft = CoolDownDuration;
            }
        }

        private void CoolDown(float timePassed)
        {
            if (_isReady == true)
            {
                return;
            }

            _timeLeft -= timePassed;
            CoolDownChanged?.Invoke(_timeLeft);

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
                _shootCount++;
                ShootCountChanged?.Invoke(_shootCount);
                _restoreTimeLeft = RestoreDuration;
            }

            if (_shootCount == MaxShootCount)
            {
                _isRestoring = false;
            }
        }
    }
}