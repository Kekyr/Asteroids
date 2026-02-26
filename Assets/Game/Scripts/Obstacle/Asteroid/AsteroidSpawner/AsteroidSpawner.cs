using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Game;
using R3;
using Random = UnityEngine.Random;

namespace Obstacle
{
    public class AsteroidSpawner
    {
        private AsteroidSpawnerData _data;
        private GameObject _container;

        private Asteroid[] _asteroids;
        private Helper _helper;
        private Score _score;

        private int _currentIndex;

        private float _currentPositionY;
        private float _currentDirectionY;

        private bool _isActive = true;

        public ReactiveProperty<Vector2> Exploded { get; } = new ReactiveProperty<Vector2>();

        public AsteroidSpawner(AsteroidSpawnerData data, Helper helper, Score score)
        {
            _data = data;
            _helper = helper;
            _score = score;
        }
        
        public void Start()
        {
            _asteroids = new Asteroid[_data.PoolCount];
            _container = new GameObject(_data.Prefab.name);
            _currentPositionY = _data.MaxPositionY;

            for (int i = 0; i < _data.PoolCount; i++)
            {
                Asteroid asteroid = GameObject.Instantiate(_data.Prefab, _container.transform);
                asteroid.gameObject.SetActive(false);

                asteroid.Construct(_helper, _data.Speed);
                asteroid.Exploded.Skip(1).Subscribe(OnExploded).AddTo(asteroid);
                _asteroids[i] = asteroid;
            }

            Spawn().Forget();
        }

        public void OnDestroy()
        {
            _isActive = false;
        }

        private async UniTask Spawn()
        {
            while (_isActive)
            {
                Asteroid asteroid = _asteroids[_currentIndex];
                asteroid.SetDirection(CalculateRandomDirection());
                asteroid.transform.position = CalculateRandomPosition();
                asteroid.gameObject.SetActive(true);
                _currentIndex++;

                if (_currentIndex >= _asteroids.Length)
                {
                    _currentIndex = 0;
                }

                await UniTask.Delay(TimeSpan.FromSeconds(_data.Delay), ignoreTimeScale: false);
            }
        }

        private Vector2 CalculateRandomPosition()
        {
            float randomXPosition = Random.Range(_data.MinPositionX, _data.MaxPositionX);
            Vector2 randomPosition = new Vector2(randomXPosition, _currentPositionY);

            if (Mathf.Approximately(_currentPositionY, _data.MaxPositionY))
            {
                _currentPositionY = _data.MinPositionY;
                _currentDirectionY = Vector2.down.y;
            }
            else
            {
                _currentPositionY = _data.MaxPositionY;
                _currentDirectionY = Vector2.up.y;
            }

            return randomPosition;
        }

        private Vector2 CalculateRandomDirection()
        {
            float randomXDirection = Random.Range(Vector2.left.x, Vector2.right.x);
            Vector2 randomDirection = new Vector2(randomXDirection, _currentDirectionY);
            return randomDirection;
        }

        private void OnExploded(Vector2 position)
        {
            _score.Add(_data.Points);
            Exploded.Value = position;
        }
    }
}