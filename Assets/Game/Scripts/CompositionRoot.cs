using System;
using Enemy;
using Obstacle;
using UnityEngine;
using Player;
using View;
using ViewModel;

namespace Game
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField] private Ship _shipPrefab;

        [SerializeField] private AsteroidSpawnerData _asteroidSpawnerData;
        [SerializeField] private AsteroidFragmentSpawnerData _asteroidFragmentSpawnerData;
        [SerializeField] private UfoSpawnerData _ufoSpawnerData;

        [SerializeField] private GunData _gunData;
        [SerializeField] private LaserGunData _laserGunData;

        [SerializeField] private Canvas _canvas;

        [SerializeField] private ShipView _shipViewPrefab;
        [SerializeField] private LaserGunView _laserGunViewPrefab;
        [SerializeField] private GameOverView _gameOverViewPrefab;

        private EntryPoint _entryPoint;

        private void Awake()
        {
            Validate();

            Helper helper = new Helper();
            Score score = new Score();
            SceneLoader sceneLoader = new SceneLoader();

            ShipData shipData = new ShipData();
            Ship ship = Instantiate(_shipPrefab);

            LaserGun laserGun = new LaserGun(_laserGunData, ship.Laser);
            Gun gun = new Gun(helper, _gunData, ship.BulletSpawnPosition);

            UfoSpawner ufoSpawner = new UfoSpawner(_ufoSpawnerData, helper, ship.transform, score);
            AsteroidSpawner asteroidSpawner = new AsteroidSpawner(_asteroidSpawnerData, helper, score);
            AsteroidFragmentSpawner asteroidFragmentSpawner =
                new AsteroidFragmentSpawner(_asteroidFragmentSpawnerData, helper, asteroidSpawner, score);

            PlayerInputRouter playerInputRouter = new PlayerInputRouter(shipData, gun, laserGun);

            ship.Construct(shipData, helper);

            ShipView shipView = Instantiate(_shipViewPrefab, _canvas.transform);
            LaserGunView laserGunView = Instantiate(_laserGunViewPrefab, _canvas.transform);
            GameOverView gameOverView = Instantiate(_gameOverViewPrefab, _canvas.transform);

            GameOverViewModel gameOverViewModel = new GameOverViewModel(score);
            LaserGunViewModel laserGunViewModel = new LaserGunViewModel(laserGun);
            ShipViewModel shipViewModel = new ShipViewModel(shipData);

            shipView.Construct(shipViewModel);
            laserGunView.Construct(laserGunViewModel);
            gameOverView.Construct(gameOverViewModel, ship, sceneLoader);

            _entryPoint = gameObject.AddComponent<EntryPoint>();
            _entryPoint.Construct(playerInputRouter, laserGun, ufoSpawner, asteroidFragmentSpawner,
                asteroidSpawner, gun);
            _entryPoint.Construct(gameOverViewModel, laserGunViewModel, shipViewModel);
        }

        private void Validate()
        {
            if (_shipPrefab == null)
            {
                throw new ArgumentNullException(nameof(_shipPrefab));
            }

            if (_asteroidSpawnerData == null)
            {
                throw new ArgumentNullException(nameof(_asteroidSpawnerData));
            }

            if (_asteroidFragmentSpawnerData == null)
            {
                throw new ArgumentNullException(nameof(_asteroidFragmentSpawnerData));
            }

            if (_ufoSpawnerData == null)
            {
                throw new ArgumentNullException(nameof(_ufoSpawnerData));
            }

            if (_gunData == null)
            {
                throw new ArgumentNullException(nameof(_gunData));
            }

            if (_laserGunData == null)
            {
                throw new ArgumentNullException(nameof(_laserGunData));
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