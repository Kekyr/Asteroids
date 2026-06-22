using System;
using AssetLoader;
using Enemy;
using Factories;
using Save;
using Obstacle;
using Player;
using UnityEngine;
using Zenject;
using IFactory = Factories.IFactory;

namespace Game
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private Transform _ui;
        [SerializeField] private Camera _camera;
        
        private LoadedAssets _loadedAssets;

        public override void InstallBindings()
        {
            Validate();

            _loadedAssets = Container.Resolve<LoadedAssets>();
            
            Container.BindInterfacesAndSelfTo<GameplayEntryPoint>().AsSingle().NonLazy();

            Container.Bind<IFactory>().To<Factory>().AsSingle();

            Container.Bind<Camera>().FromInstance(_camera).AsSingle();

            Container.Bind<Helper>().AsSingle();
            Container.Bind<Score>().AsSingle();
            Container.Bind<ShipData>().AsSingle();

            Container.Bind<Ship>().FromComponentInNewPrefab(_loadedAssets.ShipPrefab).AsSingle();
            Container.Bind<LaserGunData>().FromInstance(_loadedAssets.LaserGunData).AsSingle();
            Container.BindInterfacesAndSelfTo<LaserGun>().AsSingle();

            Container.Bind<GunData>().FromInstance(_loadedAssets.GunData).AsSingle();
            Container.BindInterfacesAndSelfTo<Gun>().AsSingle();

            Container.Bind<UfoSpawnerData>().FromInstance(_loadedAssets.UfoSpawnerData).AsSingle();
            Container.BindInterfacesAndSelfTo<UfoSpawner>().AsSingle();

            Container.Bind<AsteroidSpawnerData>().FromInstance(_loadedAssets.AsteroidSpawnerData).AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidSpawner>().AsSingle();

            Container.Bind<AsteroidFragmentSpawnerData>().FromInstance(_loadedAssets.AsteroidFragmentSpawnerData).AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidFragmentSpawner>().AsSingle();

            Container.Bind<PlayerInput>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputRouter>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameOverViewModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<LaserGunViewModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<ShipViewModel>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameOverPresenter>().AsSingle().NonLazy();

            Container.Bind<ShipView>().FromComponentInNewPrefab(_loadedAssets.ShipViewPrefab).AsSingle().NonLazy();

            Container.Bind<LaserGunView>().FromComponentInNewPrefab(_loadedAssets.LaserGunViewPrefab)
                .UnderTransform(_ui).AsSingle().NonLazy();

            Container.Bind<GameOverView>().FromComponentInNewPrefab(_loadedAssets.GameOverViewPrefab)
                .UnderTransform(_ui).AsSingle();

            Container.BindInterfacesAndSelfTo<DataCollector>().AsSingle();
            Container.BindInterfacesAndSelfTo<WinLoseController>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
        }

        private void Validate()
        {
            if (_ui == null)
            {
                throw new ArgumentNullException(nameof(_ui));
            }

            if (_camera == null)
            {
                throw new ArgumentNullException(nameof(_camera));
            }
        }
    }
}