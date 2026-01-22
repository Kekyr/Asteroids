using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Game;
using Model;

namespace Presenter
{
    public class AsteroidSpawnerPresenter : MonoBehaviour
    {
        private AsteroidPresenter _prefab;
        private GameObject _container;

        private AsteroidSpawner _model;

        private AsteroidPresenter[] _asteroids;
        private Helper _helper;
        private Score _score;

        private int _currentIndex;

        public event Action<Vector2> Exploded;

        private void Start()
        {
            _asteroids = new AsteroidPresenter[_model.PoolCount];
            _container = new GameObject(_prefab.name);

            for (int i = 0; i < _model.PoolCount; i++)
            {
                AsteroidPresenter asteroid = Instantiate(_prefab, _container.transform);
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
                Asteroid asteroid = _model.Spawn();
                AsteroidPresenter asteroidPresenter = _asteroids[_currentIndex];
                asteroidPresenter.Init(asteroid);
                asteroidPresenter.gameObject.SetActive(true);
                _currentIndex++;

                if (_currentIndex >= _asteroids.Length)
                {
                    _currentIndex = 0;
                }

                await UniTask.Delay(TimeSpan.FromSeconds(_model.Delay), ignoreTimeScale: false);
            }
        }

        public void Init(AsteroidPresenter prefab, AsteroidSpawner model, Helper helper, Score score)
        {
            _prefab = prefab;
            _model = model;
            _helper = helper;
            _score = score;
            enabled = true;
        }

        private void OnExploded(Vector2 position)
        {
            _score.Add(_model.Points);
            Exploded?.Invoke(position);
        }
    }
}