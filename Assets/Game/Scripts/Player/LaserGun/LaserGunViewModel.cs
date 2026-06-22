using System;
using Player;
using R3;
using Zenject;

namespace Player
{
    public class LaserGunViewModel : IInitializable, IDisposable
    {
        public readonly ReactiveProperty<string> ShootCount;
        public readonly ReactiveProperty<string> CoolDownTime;

        private LaserGun _model;
        private CompositeDisposable _disposables;

        public LaserGunViewModel(LaserGun model)
        {
            _model = model;
            ShootCount = new ReactiveProperty<string>();
            CoolDownTime = new ReactiveProperty<string>();
            _disposables = new CompositeDisposable();
        }

        public void Initialize()
        {
            _model.ShootCount.Subscribe(x => ShootCount.Value = $"Shoot Left: {x}").AddTo(_disposables);
            _model.CoolDownTime.Subscribe(OnCoolDownTimeChanged).AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        private void OnCoolDownTimeChanged(float value)
        {
            float roundedCoolDown = (float)Math.Round(value, 1);
            CoolDownTime.Value = $"Cool Down: {roundedCoolDown} s";
        }
    }
}