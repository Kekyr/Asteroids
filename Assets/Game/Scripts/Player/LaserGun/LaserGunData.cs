using R3;

namespace Player
{
    public class LaserGunData
    {
        private float _timeLeft;
        private float _restoreTimeLeft;

        private bool _isReady = true;

        private bool _isRestoring;

        private int _maxShootCount = 3;
        private float _coolDownDuration = 2;
        private float _shootDuration = 0.5f;
        private float _restoreDuration = 4;

        public ReactiveProperty<int> ShootCount { get; }
        public ReactiveProperty<float> CoolDownChanged { get; } = new ReactiveProperty<float>();
        public ReactiveProperty<bool> IsShooting { get; } = new ReactiveProperty<bool>();

        public LaserGunData()
        {
            ShootCount = new ReactiveProperty<int>(_maxShootCount);
        }

        public void Update(float timePassed)
        {
            Shoot(timePassed);
            CoolDown(timePassed);
            Restore(timePassed);
        }

        public void TryToShoot()
        {
            if (ShootCount.Value != 0 && _isReady == true && IsShooting.Value == false)
            {
                IsShooting.Value = true;
                _timeLeft = _shootDuration;
                _restoreTimeLeft = _restoreDuration;
            }
        }

        private void Shoot(float timePassed)
        {
            if (IsShooting.Value == false)
            {
                return;
            }

            _timeLeft -= timePassed;

            if (_timeLeft <= 0)
            {
                IsShooting.Value = false;
                _isReady = false;
                _isRestoring = true;
                ShootCount.Value--;
                _timeLeft = _coolDownDuration;
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
                _restoreTimeLeft = _restoreDuration;
            }

            if (ShootCount.Value == _maxShootCount)
            {
                _isRestoring = false;
            }
        }
    }
}