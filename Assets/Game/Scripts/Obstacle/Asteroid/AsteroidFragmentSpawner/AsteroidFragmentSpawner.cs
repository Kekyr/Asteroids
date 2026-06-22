using System;
using UnityEngine;
using Game;
using R3;
using Zenject;
using IFactory = Factories.IFactory;
using Random = UnityEngine.Random;

namespace Obstacle
{
    public class AsteroidFragmentSpawner : IInitializable, IDisposable
    {
        private AsteroidFragmentSpawnerData _data;
        private GameObject _container;
        private IFactory _factory;

        private Helper _helper;
        private Score _score;
        private AsteroidSpawner _asteroidSpawner;
        private Asteroid[] _asteroidFragments;

        private int _currentIndex;
        private IDisposable _disposable;

        private int _destroyedCount;

        public int DestroyedCount => _destroyedCount;

        public AsteroidFragmentSpawner(AsteroidFragmentSpawnerData data, Helper helper,
            AsteroidSpawner asteroidSpawner, Score score, IFactory factory)
        {
            _data = data;
            _helper = helper;
            _asteroidSpawner = asteroidSpawner;
            _score = score;
            _factory = factory;
        }

        public void Initialize()
        {
            _asteroidFragments = new Asteroid[_data.PoolCount];
            _container = new GameObject(_data.Prefab.name);

            for (int i = 0; i < _data.PoolCount; i++)
            {
                Asteroid asteroid = _factory.Create(_data.Prefab, _container.transform);
                asteroid.gameObject.SetActive(false);

                asteroid.IsExploded.Subscribe(x => OnFragmentExploded()).AddTo(asteroid);
                _asteroidFragments[i] = asteroid;
                asteroid.Construct(_helper, _data.Speed);
            }

            _disposable = _asteroidSpawner.Exploded.Subscribe(OnExploded);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private Vector2 CalculateRandomPosition(Vector2 position)
        {
            float randomXPosition =
                Random.Range(position.x - _data.PositionXOffset, position.x + _data.PositionXOffset);
            float randomYPosition =
                Random.Range(position.y - _data.PositionYOffset, position.y + _data.PositionYOffset);
            Vector2 randomPosition = new Vector2(randomXPosition, randomYPosition);
            return randomPosition;
        }

        private Vector2 CalculateRandomDirection()
        {
            float randomXDirection = Random.Range(Vector2.left.x, Vector2.right.x);
            float randomYDirection = Random.Range(Vector2.down.y, Vector2.up.y);
            Vector2 randomDirection = new Vector2(randomXDirection, randomYDirection);
            return randomDirection;
        }

        private void OnExploded(Vector2 position)
        {
            for (int i = 0; i < _data.ExplodeCount; i++)
            {
                Asteroid asteroid = _asteroidFragments[_currentIndex];
                asteroid.SetDirection(CalculateRandomDirection());
                asteroid.transform.position = CalculateRandomPosition(position);
                asteroid.gameObject.SetActive(true);
                _currentIndex++;

                if (_currentIndex >= _asteroidFragments.Length)
                {
                    _currentIndex = 0;
                }
            }
        }

        private void OnFragmentExploded()
        {
            _score.Add(_data.Points);
            _destroyedCount++;
        }
    }
}