using System;
using UnityEngine;
using TMPro;
using Model;

namespace View
{
    public class LaserGunView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _shootCountText;
        [SerializeField] private TextMeshProUGUI _coolDownText;

        private LaserGun _model;
        
        private void OnDestroy()
        {
            _model.ShootCountChanged -= OnShootCountChanged;
            _model.CoolDownChanged -= OnCoolDownChanged;
        }

        public void Init(LaserGun laserGun)
        {
            _model = laserGun;
            _model.ShootCountChanged += OnShootCountChanged;
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