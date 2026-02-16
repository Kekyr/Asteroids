using System;
using Enemy;
using Obstacle;
using UnityEngine;
using Player;
using View;

namespace Game
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField] private Ship _shipPrefab;

        [SerializeField] private Asteroid _asteroidPrefab;
        [SerializeField] private Asteroid _asteroidFragmentPrefab;
        [SerializeField] private Ufo _ufoPrefab;

        [SerializeField] private Canvas _canvas;

        [SerializeField] private ShipView _shipViewPrefab;
        [SerializeField] private LaserGunView _laserGunViewPrefab;
        [SerializeField] private GameOverView _gameOverViewPrefab;

        private EntryPoint _entryPoint;

        private PlayerInputRouter _playerInputRouter;
        private ShipData _shipDataModel;
        private LaserGunData _laserGunData;
        private AsteroidSpawnerData _asteroidSpawnerData;
        private AsteroidFragmentSpawnerData _asteroidFragmentSpawnerData;
        private UfoSpawner _ufoSpawner;
        private Score _score;
        private Helper _helper;

        private Ship _ship;
        private Gun _gun;
        private LaserGun _laserGun;
        private AsteroidSpawner _asteroidSpawner;
        private AsteroidFragmentSpawner _asteroidFragmentSpawner;

        private ShipView _shipView;
        private LaserGunView _laserGunView;
        private GameOverView _gameOverView;

        private void Awake()
        {
            Validate();

            _helper = new Helper();
            _score = new Score();

            _shipDataModel = new ShipData();
            _laserGunData = new LaserGunData();

            _asteroidSpawnerData = new AsteroidSpawnerData();
            _asteroidFragmentSpawnerData = new AsteroidFragmentSpawnerData();

            _ship = Instantiate(_shipPrefab);

            _ufoSpawner = new UfoSpawner(_ufoPrefab, _helper, _ship.transform, _score);

            _gun = _ship.gameObject.GetComponentInChildren<Gun>();
            _laserGun = _ship.gameObject.GetComponentInChildren<LaserGun>();
            
            _playerInputRouter = new PlayerInputRouter(_shipDataModel, _gun, _laserGunData);

            _ship.Init(_shipDataModel, _helper);
            _gun.Init(_helper);
            _laserGun.Init(_laserGunData);

            _asteroidSpawner = gameObject.AddComponent<AsteroidSpawner>();
            _asteroidFragmentSpawner = gameObject.AddComponent<AsteroidFragmentSpawner>();

            _asteroidSpawner.Init(_asteroidPrefab, _asteroidSpawnerData, _helper, _score);
            _asteroidFragmentSpawner.Init(_asteroidFragmentPrefab, _asteroidFragmentSpawnerData, _helper,
                _asteroidSpawner, _score);

            _shipView = Instantiate(_shipViewPrefab, _canvas.transform);
            _laserGunView = Instantiate(_laserGunViewPrefab, _canvas.transform);
            _gameOverView = Instantiate(_gameOverViewPrefab, _canvas.transform);

            _shipView.Init(_shipDataModel);
            _laserGunView.Init(_laserGunData);
            _gameOverView.Init(_score, _ship);

            _entryPoint = gameObject.AddComponent<EntryPoint>();
            _entryPoint.Init(_playerInputRouter, _laserGunData, _ufoSpawner);
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