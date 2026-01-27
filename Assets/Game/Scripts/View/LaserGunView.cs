using System;
using UnityEngine;
using TMPro;
using Model;
using R3;

namespace View
{
    public class LaserGunView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _shootCountText;
        [SerializeField] private TextMeshProUGUI _coolDownText;

        private LaserGun _model;

        private IDisposable _shootCountChanged;

        private void OnDestroy()
        {
            _shootCountChanged.Dispose();
            _model.CoolDownChanged -= OnCoolDownChanged;
        }

        public void Init(LaserGun laserGun)
        {
            _model = laserGun;
            _shootCountChanged = _model.ShootCount.Subscribe(OnShootCountChanged);
            _model.CoolDownChanged += OnCoolDownChanged;
            enabled = true;
        }

        private void OnShootCountChanged(int shootCount)
        {
            _shootCountText.text = $"Shoot Left: {shootCount}";
        }

        private void OnCoolDownChanged(float coolDown)
        {
            float roundedCoolDown = (float)Math.Round(coolDown, 1);
            _coolDownText.text = $"Cool Down: {roundedCoolDown} s";
        }
    }
}