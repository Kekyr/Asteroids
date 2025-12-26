using System;
using UnityEngine;
using Model;
using Presenter;
using View;

namespace Game
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private ShipPresenter _shipPresenter;
        [SerializeField] private AsteroidSpawnerPresenter _asteroidSpawnerPresenter;
        [SerializeField] private AsteroidFragmentSpawnerPresenter _asteroidFragmentSpawnerPresenter;
        [SerializeField] private UfoSpawnerPresenter _ufoSpawnerPresenter;

        [SerializeField] private ShipView _shipView;
        [SerializeField] private LaserGunView _laserGunView;
        [SerializeField] private GameOverView _gameOverView;
        
        private PlayerInputRouter _playerInputRouter;
        private Ship _ship;
        private Gun _gun;
        private LaserGun _laserGun;
        private AsteroidSpawner _asteroidSpawner;
        private AsteroidFragmentSpawner _asteroidFragmentSpawner;
        private UfoSpawner _ufoSpawner;
        private Score _score;

        private GunPresenter _gunPresenter;
        private LaserGunPresenter _laserGunPresenter;
        
        private Helper _helper;

        private void Awake()
        {
            Validate();

            _helper = new Helper();

            _ship = new Ship();
            _gun = new Gun();
            _laserGun = new LaserGun();

            _asteroidSpawner = new AsteroidSpawner();
            _asteroidFragmentSpawner = new AsteroidFragmentSpawner();
            _ufoSpawner = new UfoSpawner();

            _playerInputRouter = new PlayerInputRouter(_ship, _gun, _laserGun);

            _score = new Score();

            _gunPresenter = _shipPresenter.gameObject.GetComponentInChildren<GunPresenter>();
            _laserGunPresenter = _shipPresenter.gameObject.GetComponentInChildren<LaserGunPresenter>();

            _shipPresenter.Init(_ship, _helper);
            _gunPresenter.Init(_gun, _helper);
            _laserGunPresenter.Init(_laserGun);

            _asteroidSpawnerPresenter.Init(_asteroidSpawner, _helper, _score);
            _asteroidFragmentSpawnerPresenter.Init(_asteroidFragmentSpawner, _helper, _asteroidSpawnerPresenter,
                _score);
            _ufoSpawnerPresenter.Init(_ufoSpawner, _helper, _shipPresenter.transform, _score);

            _shipView.Init(_ship);
            _laserGunView.Init(_laserGun);
            _gameOverView.Init(_score, _shipPresenter);
        }

        private void OnEnable()
        {
            _playerInputRouter.OnEnable();
            _laserGun.OnEnable();
        }

        private void OnDisable()
        {
            _playerInputRouter.OnDisable();
        }

        private void Update()
        {
            _laserGun.Update(Time.deltaTime);
        }

        private void Validate()
        {
            if (_shipPresenter == null)
            {
                throw new ArgumentNullException(nameof(_shipPresenter));
            }
        }
    }
}