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

        [SerializeField] private AsteroidSpawnerData _asteroidSpawnerData;
        [SerializeField] private AsteroidFragmentSpawnerData _asteroidFragmentSpawnerData;
        [SerializeField] private UfoSpawnerData _ufoSpawnerData;

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

            ShipData shipDataModel = new ShipData();
            LaserGunData laserGunData = new LaserGunData();

            Ship ship = Instantiate(_shipPrefab);

            UfoSpawner ufoSpawner = new UfoSpawner(_ufoSpawnerData, helper, ship.transform, score);

            Gun gun = ship.gameObject.GetComponentInChildren<Gun>();
            LaserGun laserGun = ship.gameObject.GetComponentInChildren<LaserGun>();

            PlayerInputRouter playerInputRouter = new PlayerInputRouter(shipDataModel, gun, laserGunData);

            ship.Construct(shipDataModel, helper);
            gun.Construct(helper);
            laserGun.Construct(laserGunData);

            AsteroidSpawner asteroidSpawner = gameObject.AddComponent<AsteroidSpawner>();
            AsteroidFragmentSpawner asteroidFragmentSpawner = gameObject.AddComponent<AsteroidFragmentSpawner>();

            asteroidSpawner.Construct(_asteroidSpawnerData, helper, score);
            asteroidFragmentSpawner.Construct(_asteroidFragmentSpawnerData, helper,
                asteroidSpawner, score);

            ShipView shipView = Instantiate(_shipViewPrefab, _canvas.transform);
            LaserGunView laserGunView = Instantiate(_laserGunViewPrefab, _canvas.transform);
            GameOverView gameOverView = Instantiate(_gameOverViewPrefab, _canvas.transform);

            shipView.Construct(shipDataModel);
            laserGunView.Construct(laserGunData);
            gameOverView.Construct(score, ship, sceneLoader);

            _entryPoint = gameObject.AddComponent<EntryPoint>();
            _entryPoint.Construct(playerInputRouter, laserGunData, ufoSpawner);
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