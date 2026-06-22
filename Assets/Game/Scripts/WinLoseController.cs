using System;
using Analytics;
using Enemy;
using Save;
using Obstacle;
using Player;
using R3;
using Zenject;

namespace Game
{
    public class WinLoseController : IInitializable, IDisposable
    {
        private Ship _ship;
        private Score _score;
        private Gun _gun;
        private LaserGun _laserGun;
        private UfoSpawner _ufoSpawner;
        private AsteroidSpawner _asteroidSpawner;
        private AsteroidFragmentSpawner _asteroidFragmentSpawner;
        private DataCollector _dataCollector;
        private GameOverView _gameOverView;
        private IDisposable _disposable;
        private IAnalytics _analytics;
        
        public ReactiveProperty<bool> IsGameOver { get; } = new ReactiveProperty<bool>();

        public WinLoseController(Ship ship, GameOverView gameOverView, Score score, DataCollector dataCollector,
            UfoSpawner ufoSpawner, AsteroidSpawner asteroidSpawner,
            AsteroidFragmentSpawner asteroidFragmentSpawner, Gun gun, LaserGun laserGun, IAnalytics analytics)
        {
            _ship = ship;
            _gameOverView = gameOverView;
            _score = score;
            _dataCollector = dataCollector;
            _ufoSpawner = ufoSpawner;
            _asteroidSpawner = asteroidSpawner;
            _asteroidFragmentSpawner = asteroidFragmentSpawner;
            _gun = gun;
            _laserGun = laserGun;
            _analytics = analytics;
        }

        public void Initialize()
        {
            _disposable = _ship.IsDestroyed.Subscribe(x => OnShipDestroyed(x));
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void OnShipDestroyed(bool isDestroyed)
        {
            if (isDestroyed == false)
            {
                return;
            }

            IsGameOver.Value = true;

            if (_score.CurrentScore.Value > _score.HighScore.Value)
            {
                _dataCollector.Save();
            }

            int totalEnemiesDestroyed = _ufoSpawner.DestroyedCount + _asteroidSpawner.DestroyedCount + _asteroidFragmentSpawner.DestroyedCount;
            _analytics.LogGameEnd(_gun.TotalShootCount, _laserGun.TotalShootCount, totalEnemiesDestroyed);
        }
    }
}