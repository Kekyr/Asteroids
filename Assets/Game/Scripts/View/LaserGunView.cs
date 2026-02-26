using System;
using Player;
using UnityEngine;
using TMPro;
using R3;

namespace View
{
    public class LaserGunView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _shootCountText;
        [SerializeField] private TextMeshProUGUI _coolDownText;

        private LaserGunData _model;

        public void Construct(LaserGunData laserGunData)
        {
            _model = laserGunData;
            _model.ShootCount.Subscribe(OnShootCountChanged).AddTo(this);
            _model.CoolDownChanged.Subscribe(OnCoolDownChanged).AddTo(this);
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