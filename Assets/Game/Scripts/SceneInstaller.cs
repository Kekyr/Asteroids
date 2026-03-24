using System;
using Enemy;
using Obstacle;
using Player;
using UnityEngine;
using View;
using ViewModel;
using Zenject;

namespace Game
{
    public class SceneInstaller : MonoInstaller
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

        public override void InstallBindings()
        {
            Validate();

            Container.Bind<Helper>().AsSingle();
            Container.Bind<Score>().AsSingle();
            Container.Bind<ShipData>().AsSingle();

            Container.Bind<Ship>().FromComponentInNewPrefab(_shipPrefab).AsSingle();

            Container.Bind<LaserGunData>().FromInstance(_laserGunData).AsSingle();
            Container.BindInterfacesAndSelfTo<LaserGun>().AsSingle();

            Container.Bind<GunData>().FromInstance(_gunData).AsSingle();
            Container.BindInterfacesAndSelfTo<Gun>().AsSingle();

            Container.Bind<UfoSpawnerData>().FromInstance(_ufoSpawnerData).AsSingle();
            Container.BindInterfacesAndSelfTo<UfoSpawner>().AsSingle();

            Container.Bind<AsteroidSpawnerData>().FromInstance(_asteroidSpawnerData).AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidSpawner>().AsSingle();

            Container.Bind<AsteroidFragmentSpawnerData>().FromInstance(_asteroidFragmentSpawnerData).AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidFragmentSpawner>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerInputRouter>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameOverViewModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<LaserGunViewModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<ShipViewModel>().AsSingle();

            Container.Bind<ShipView>().FromComponentInNewPrefab(_shipViewPrefab).UnderTransform(_canvas.transform)
                .AsSingle().NonLazy();
            Container.Bind<LaserGunView>().FromComponentInNewPrefab(_laserGunViewPrefab)
                .UnderTransform(_canvas.transform).AsSingle().NonLazy();
            Container.Bind<GameOverView>().FromComponentInNewPrefab(_gameOverViewPrefab)
                .UnderTransform(_canvas.transform).AsSingle();

            Container.BindInterfacesAndSelfTo<WinLoseController>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
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