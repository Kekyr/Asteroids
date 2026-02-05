using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Game;

namespace Obstacle
{
    public class AsteroidSpawner : MonoBehaviour
    {
        private Asteroid _prefab;
        private GameObject _container;

        private AsteroidSpawnerData _model;

        private Asteroid[] _asteroids;
        private Helper _helper;
        private Score _score;

        private int _currentIndex;

        public event Action<Vector2> Exploded;

        private void Start()
        {
            _asteroids = new Asteroid[_model.PoolCount];
            _container = new GameObject(_prefab.name);

            for (int i = 0; i < _model.PoolCount; i++)
            {
                Asteroid asteroid = Instantiate(_prefab, _container.transform);
                asteroid.gameObject.SetActive(false);
                
                asteroid.Init(_helper, _model.Speed);
                asteroid.Exploded += OnExploded;
                _asteroids[i] = asteroid;
            }
            
            Spawn().Forget();
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _asteroids.Length; i++)
            {
                _asteroids[i].Exploded -= OnExploded;
            }
        }

        private async UniTask Spawn()
        {
            while (isActiveAndEnabled)
            {
                AsteroidData asteroidData = _model.Spawn();
                Asteroid asteroid = _asteroids[_currentIndex];
                asteroid.Init(asteroidData);
                asteroid.gameObject.SetActive(true);
                _currentIndex++;

                if (_currentIndex >= _asteroids.Length)
                {
                    _currentIndex = 0;
                }

                await UniTask.Delay(TimeSpan.FromSeconds(_model.Delay), ignoreTimeScale: false);
            }
        }

        public void Init(Asteroid prefab, AsteroidSpawnerData model, Helper helper, Score score)
        {
            _prefab = prefab;
            _model = model;
            _helper = helper;
            _score = score;
        }

        private void OnExploded(Vector2 position)
        {
            _score.Add(_model.Points);
            Exploded?.Invoke(position);
        }
    }
}