using System;
using UnityEngine;
using Model;
using Presenter;
using View;

namespace Game
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private GameObject _shipPrefab;

        [SerializeField] private GameObject _asteroidPrefab;
        [SerializeField] private GameObject _asteroidFragmentPrefab;
        [SerializeField] private GameObject _ufoPrefab;

        [SerializeField] private Canvas _canvas;

        [SerializeField] private GameObject _shipViewPrefab;
        [SerializeField] private GameObject _laserGunViewPrefab;
        [SerializeField] private GameObject _gameOverViewPrefab;

        private GameObject _ship;

        private GameObject _shipViewObject;
        private GameObject _laserGunViewObject;
        private GameObject _gameOverViewObject;

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
        private UfoSpawnerPresenter _ufoSpawnerPresenter;

        private ShipView _shipView;
        private LaserGunView _laserGunView;
        private GameOverView _gameOverView;

        private void Awake()
        {
            Validate();

            _helper = new Helper();

            _shipModel = new Ship();
            _gun = new Gun();
            _laserGun = new LaserGun();

            _asteroidSpawner = new AsteroidSpawner();
            _asteroidFragmentSpawner = new AsteroidFragmentSpawner();
            _ufoSpawner = new UfoSpawner();

            _playerInputRouter = new PlayerInputRouter(_shipModel, _gun, _laserGun);

            _score = new Score();

            _ship = Instantiate(_shipPrefab);

            _shipPresenter = _ship.GetComponent<ShipPresenter>();
            _gunPresenter = _ship.GetComponentInChildren<GunPresenter>();
            _laserGunPresenter = _ship.GetComponentInChildren<LaserGunPresenter>();

            _shipPresenter.Init(_shipModel, _helper);
            _gunPresenter.Init(_gun, _helper);
            _laserGunPresenter.Init(_laserGun);

            _asteroidSpawnerPresenter = gameObject.AddComponent<AsteroidSpawnerPresenter>();
            _asteroidFragmentSpawnerPresenter = gameObject.AddComponent<AsteroidFragmentSpawnerPresenter>();
            _ufoSpawnerPresenter = gameObject.AddComponent<UfoSpawnerPresenter>();

            _asteroidSpawnerPresenter.Init(_asteroidPrefab, _asteroidSpawner, _helper, _score);
            _asteroidFragmentSpawnerPresenter.Init(_asteroidFragmentPrefab, _asteroidFragmentSpawner, _helper,
                _asteroidSpawnerPresenter, _score);
            _ufoSpawnerPresenter.Init(_ufoPrefab, _ufoSpawner, _helper, _shipPresenter.transform, _score);

            _shipViewObject = Instantiate(_shipViewPrefab, _canvas.transform);
            _shipView = _shipViewObject.GetComponent<ShipView>();

            _laserGunViewObject = Instantiate(_laserGunViewPrefab, _canvas.transform);
            _laserGunView = _laserGunViewObject.GetComponent<LaserGunView>();

            _gameOverViewObject = Instantiate(_gameOverViewPrefab, _canvas.transform);
            _gameOverView = _gameOverViewObject.GetComponent<GameOverView>();

            _shipView.Init(_shipModel);
            _laserGunView.Init(_laserGun);
            _gameOverView.Init(_score, _shipPresenter);
        }

        private void Start()
        {
            _playerInputRouter.Start();
            _laserGun.Start();
        }

        private void OnDestroy()
        {
            _playerInputRouter.OnDestroy();
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