using PlayerBase;
using TMPro;
using UnityEngine;

namespace UI
{
    public class LaserGunView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _shootCountText;
        [SerializeField] private TextMeshProUGUI _coolDownText;

        private LaserGun _laserGun;

        private void Start()
        {
            _laserGun.ShootCountChanged += OnShootCountChanged;
            _laserGun.CoolDownChanged += OnCoolDownChanged;
        }

        private void OnDestroy()
        {
            _laserGun.ShootCountChanged -= OnShootCountChanged;
            _laserGun.CoolDownChanged -= OnCoolDownChanged;
        }

        public void Init(LaserGun laserGun)
        {
            _laserGun = laserGun;
            enabled = true;
        }

        private void OnShootCountChanged(int shootCount)
        {
            _shootCountText.text = $"Shoot Left: {shootCount}";
        }

        private void OnCoolDownChanged(float coolDown)
        {
            _coolDownText.text = $"Cool Down: {coolDown} s";
        }
    }
}
