using System;
using Player;
using R3;

namespace ViewModel
{
    public class LaserGunViewModel
    {
        public readonly ReactiveProperty<string> ShootCount;
        public readonly ReactiveProperty<string> CoolDownTime;

        private CompositeDisposable _disposables;

        public LaserGunViewModel(LaserGun laserGun)
        {
            ShootCount = new ReactiveProperty<string>();
            CoolDownTime = new ReactiveProperty<string>();
            _disposables = new CompositeDisposable();
            
            laserGun.ShootCount.Subscribe(x => ShootCount.Value = $"Shoot Left: {x}").AddTo(_disposables);
            laserGun.CoolDownTime.Subscribe(OnCoolDownTimeChanged).AddTo(_disposables);
        }

        public void OnDestroy()
        {
            _disposables.Dispose();
        }

        private void OnCoolDownTimeChanged(float value)
        {
            float roundedCoolDown = (float)Math.Round(value, 1);
            CoolDownTime.Value= $"Cool Down: {roundedCoolDown} s";
        }
    }
}