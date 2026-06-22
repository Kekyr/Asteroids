using System;
using Cysharp.Threading.Tasks;
using Enemy;
using Obstacle;
using Player;
using UnityEngine;

namespace AssetLoader
{
    public class LoadedAssets : IDisposable
    {
        private IAssetLoader _assetLoader;

        public GameObject ShipPrefab { get; private set; }
        public GameObject ShipViewPrefab { get; private set; }
        public GameObject LaserGunViewPrefab { get; private set; }
        public GameObject GameOverViewPrefab { get; private set; }
        public AsteroidSpawnerData AsteroidSpawnerData { get; private set; }
        public AsteroidFragmentSpawnerData AsteroidFragmentSpawnerData { get; private set; }
        public UfoSpawnerData UfoSpawnerData { get; private set; }

        public GunData GunData { get; private set; }
        public LaserGunData LaserGunData { get; private set; }

        public LoadedAssets(IAssetLoader assetLoader)
        {
            _assetLoader = assetLoader;
        }

        public void Dispose()
        {
            UnLoadAssets();
        }

        public async UniTask LoadAssets()
        {
            var task1 = _assetLoader.Load<GameObject>("Ship");
            var task2 = _assetLoader.Load<GameObject>("ShipView");
            var task3 = _assetLoader.Load<GameObject>("LaserGunView");
            var task4 = _assetLoader.Load<GameObject>("GameOverView");
            var task5 = _assetLoader.Load<AsteroidSpawnerData>("AsteroidSpawnerData");
            var task6 = _assetLoader.Load<AsteroidFragmentSpawnerData>("AsteroidFragmentSpawnerData");
            var task7 = _assetLoader.Load<UfoSpawnerData>("UfoSpawnerData");
            var task8 = _assetLoader.Load<GunData>("GunData");
            var task9 = _assetLoader.Load<LaserGunData>("LaserGunData");

            var (ship, shipView, laserGunView, gameOverView, asteroidSpawnerData, asteroidFragmentSpawnerData,
                    ufoSpawnerData, gunData, laserGunData) =
                await UniTask.WhenAll(task1, task2, task3, task4, task5, task6, task7, task8, task9);

            ShipPrefab = ship;
            ShipViewPrefab = shipView;
            LaserGunViewPrefab = laserGunView;
            GameOverViewPrefab = gameOverView;
            AsteroidSpawnerData = asteroidSpawnerData;
            AsteroidFragmentSpawnerData = asteroidFragmentSpawnerData;
            UfoSpawnerData = ufoSpawnerData;
            GunData = gunData;
            LaserGunData = laserGunData;
        }

        public void UnLoadAssets()
        {
            _assetLoader.UnLoad(ShipPrefab);
            _assetLoader.UnLoad(ShipViewPrefab);
            _assetLoader.UnLoad(LaserGunViewPrefab);
            _assetLoader.UnLoad(GameOverViewPrefab);
            _assetLoader.UnLoad(AsteroidSpawnerData);
            _assetLoader.UnLoad(AsteroidFragmentSpawnerData);
            _assetLoader.UnLoad(UfoSpawnerData);
            _assetLoader.UnLoad(GunData);
            _assetLoader.UnLoad(LaserGunData);
        }
    }
}