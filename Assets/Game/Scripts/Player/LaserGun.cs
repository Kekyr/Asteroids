using UnityEngine;

namespace Player
{
    public class LaserGun : MonoBehaviour
    {
        [SerializeField] private GameObject _laser;

        private LaserGunData _model;

        private void Start()
        {
            _model.ShootStarted += OnShootStarted;
            _model.ShootEnded += OnShootEnded;
        }

        private void OnDestroy()
        {
            _model.ShootStarted -= OnShootStarted;
            _model.ShootEnded -= OnShootEnded;
        }

        public void Init(LaserGunData model)
        {
            _model = model;
        }

        private void OnShootStarted()
        {
            _laser.SetActive(true);
        }

        private void OnShootEnded()
        {
            _laser.SetActive(false);
        }
    }
}