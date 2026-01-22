using System;
using UnityEngine;
using Model;
using Presenter;
using View;

namespace Game
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private ShipPresenter _shipPrefab;

        [SerializeField] private AsteroidPresenter _asteroidPrefab;
        [SerializeField] private AsteroidPresenter _asteroidFragmentPrefab;
        [SerializeField] private UfoPresenter _ufoPrefab;

        [SerializeField] private Canvas _canvas;

        [SerializeField] private ShipView _shipViewPrefab;
        [SerializeField] private LaserGunView _laserGunViewPrefab;
        [SerializeField] private GameOverView _gameOverViewPrefab;

        private PlayerInputRouter _playerInputRouter;
        private Ship _shipModel;
        private Gun _gun;
        private LaserGun _laserGun;
        private AsteroidSpawner _asteroidSpawner;
        private AsteroidFragmentSpawner _asteroidFragmentSpawner;
        private UfoSpawner _ufoSpawner;
        private Score _score;
        private Helper _helper;

        private ShipPresenter _shipPresenter;
        private GunPresenter _gunPresenter;
        private LaserGunPresenter _laserGunPresenter;
        private AsteroidSpawnerPresenter _asteroidSpawnerPresenter;
        private AsteroidFragmentSpawnerPresenter _asteroidFragmentSpawnerPresenter;

        private ShipView _shipView;
        private LaserGunView _laserGunView;
        private GameOverView _gameOverView;

        private void Awake()
        {
            Validate();

            _helper = new Helper();
            _score = new Score();

            _shipModel = new Ship();
            _gun = new Gun();
            _laserGun = new LaserGun();

            _asteroidSpawner = new AsteroidSpawner();
            _asteroidFragmentSpawner = new AsteroidFragmentSpawner();

            _playerInputRouter = new PlayerInputRouter(_shipModel, _gun, _laserGun);

            _shipPresenter = Instantiate(_shipPrefab);

            _ufoSpawner = new UfoSpawner(_ufoPrefab, _helper, _shipPresenter.transform, _score);
            
            _gunPresenter = _shipPresenter.gameObject.GetComponentInChildren<GunPresenter>();
            _laserGunPresenter = _shipPresenter.gameObject.GetComponentInChildren<LaserGunPresenter>();

            _shipPresenter.Init(_shipModel, _helper);
            _gunPresenter.Init(_gun, _helper);
            _laserGunPresenter.Init(_laserGun);

            _asteroidSpawnerPresenter = gameObject.AddComponent<AsteroidSpawnerPresenter>();
            _asteroidFragmentSpawnerPresenter = gameObject.AddComponent<AsteroidFragmentSpawnerPresenter>();

            _asteroidSpawnerPresenter.Init(_asteroidPrefab, _asteroidSpawner, _helper, _score);
            _asteroidFragmentSpawnerPresenter.Init(_asteroidFragmentPrefab, _asteroidFragmentSpawner, _helper,_asteroidSpawnerPresenter, _score);

            _shipView = Instantiate(_shipViewPrefab, _canvas.transform);
            _laserGunView = Instantiate(_laserGunViewPrefab, _canvas.transform);
            _gameOverView = Instantiate(_gameOverViewPrefab, _canvas.transform);

            _shipView.Init(_shipModel);
            _laserGunView.Init(_laserGun);
            _gameOverView.Init(_score, _shipPresenter);
        }

        private void Start()
        {
            _playerInputRouter.Start();
            _laserGun.Start();
            _ufoSpawner.Start();
        }

        private void OnDestroy()
        {
            _playerInputRouter.OnDestroy();
            _ufoSpawner.OnDestroy();
        }

        private void Update()
        {
            _laserGun.Update(Time.deltaTime);
        }

        private void Validate()
        {
            if (_shipPrefab == null)
            {
                throw new ArgumentNullException(nameof(_shipPrefab));
            }

            if (_asteroidPrefab == null)
            {
                throw new ArgumentNullException(nameof(_asteroidPrefab));
            }

            if (_asteroidFragmentPrefab == null)
            {
                throw new ArgumentNullException(nameof(_asteroidFragmentPrefab));
            }

            if (_ufoPrefab == null)
            {
                throw new ArgumentNullException(nameof(_ufoPrefab));
            }

            if (_canvas == null)
            {
                throw new ArgumentNullException(nameof(_canvas));
            }

            if (_shipViewPrefab == null)
            {
                throw new ArgumentNullException(nameof(_shipViewPrefab));
            }

            if (_laserGunViewPrefab == null)
            {
                throw new ArgumentNullException(nameof(_laserGunViewPrefab));
            }

            if (_gameOverViewPrefab == null)
            {
                throw new ArgumentNullException(nameof(_gameOverViewPrefab));
            }
        }
    }
}