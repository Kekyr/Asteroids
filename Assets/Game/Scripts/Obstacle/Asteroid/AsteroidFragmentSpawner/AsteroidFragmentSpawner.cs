using System;
using UnityEngine;
using Game;
using R3;
using Random = UnityEngine.Random;

namespace Obstacle
{
    public class AsteroidFragmentSpawner
    {
        private AsteroidFragmentSpawnerData _data;
        private GameObject _container;

        private Helper _helper;
        private Score _score;
        private AsteroidSpawner _asteroidSpawner;
        private Asteroid[] _asteroidFragments;

        private int _currentIndex;
        private IDisposable _disposable;

        public AsteroidFragmentSpawner(AsteroidFragmentSpawnerData data, Helper helper,
            AsteroidSpawner asteroidSpawner, Score score)
        {
            _data = data;
            _helper = helper;
            _asteroidSpawner = asteroidSpawner;
            _score = score;
        }

        public void Start()
        {
            _asteroidFragments = new Asteroid[_data.PoolCount];
            _container = new GameObject(_data.Prefab.name);

            for (int i = 0; i < _data.PoolCount; i++)
            {
                Asteroid asteroid = GameObject.Instantiate(_data.Prefab, _container.transform);
                asteroid.gameObject.SetActive(false);

                asteroid.Exploded.Subscribe(OnFragmentExploded).AddTo(asteroid);
                _asteroidFragments[i] = asteroid;
                asteroid.Construct(_helper, _data.Speed);
            }

            _disposable = _asteroidSpawner.Exploded.Skip(1).Subscribe(OnExploded);
        }

        public void OnDestroy()
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

        private void OnFragmentExploded(Vector2 position)
        {
            _score.Add(_data.Points);
        }
    }
}