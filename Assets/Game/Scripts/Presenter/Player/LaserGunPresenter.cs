using UnityEngine;
using Model;

namespace Presenter
{
    public class LaserGunPresenter : MonoBehaviour
    {
        [SerializeField] private GameObject _laser;

        private LaserGun _model;

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

        public void Init(LaserGun model)
        {
            _model = model;
            enabled = true;
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