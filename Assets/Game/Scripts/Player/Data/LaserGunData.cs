using System;
using R3;

namespace Player
{
    public class LaserGunData
    {
        private float _timeLeft;
        private float _restoreTimeLeft;

        private bool _isReady = true;
        private bool _isShooting;
        private bool _isRestoring;

        public int MaxShootCount { get; } = 3;
        public float CoolDownDuration { get; } = 2;
        public float ShootDuration { get; } = 0.5f;
        public float RestoreDuration { get; } = 4;

        public ReactiveProperty<int> ShootCount { get; }

        public event Action<float> CoolDownChanged;

        public event Action ShootStarted;
        public event Action ShootEnded;

        public LaserGunData()
        {
            ShootCount = new ReactiveProperty<int>();
        }

        public void Start()
        {
            ShootCount.Value = MaxShootCount;
        }

        public void Update(float timePassed)
        {
            Shoot(timePassed);
            CoolDown(timePassed);
            Restore(timePassed);
        }

        public void TryToShoot()
        {
            if (ShootCount.Value != 0 && _isReady == true && _isShooting == false)
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
                ShootCount.Value--;
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
                ShootCount.Value++;
                _restoreTimeLeft = RestoreDuration;
            }

            if (ShootCount.Value == MaxShootCount)
            {
                _isRestoring = false;
            }
        }
    }
}