using R3;
using UnityEngine;

namespace Player
{
    public class LaserGun : MonoBehaviour
    {
        [SerializeField] private GameObject _laser;

        private LaserGunData _model;

        private void Start()
        {
            _model.IsShooting.Subscribe(OnShoot).AddTo(this);
        }

        public void Init(LaserGunData model)
        {
            _model = model;
        }

        private void OnShoot(bool isStarted)
        {
            if (isStarted)
            {
                _laser.SetActive(true);
            }
            else
            {
                _laser.SetActive(false);
            }
        }
    }
}