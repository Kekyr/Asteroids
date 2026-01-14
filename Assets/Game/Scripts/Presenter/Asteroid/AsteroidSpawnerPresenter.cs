using System;
using System.Collections;
using UnityEngine;
using Game;
using Model;

namespace Presenter
{
    public class AsteroidSpawnerPresenter : MonoBehaviour
    {
        private GameObject _prefab;
        private GameObject _container;

        private AsteroidSpawner _model;

        private AsteroidPresenter[] _asteroids;
        private Helper _helper;
        private Score _score;
        private WaitForSeconds _waitDelay;

        private int _currentIndex;

        public event Action<Vector2> Exploded;

        private void Start()
        {
            _waitDelay = new WaitForSeconds(_model.Delay);
            _asteroids = new AsteroidPresenter[_model.PoolCount];
            _container = new GameObject(_prefab.name);

            for (int i = 0; i < _model.PoolCount; i++)
            {
                GameObject instance = Instantiate(_prefab, _container.transform);
                instance.SetActive(false);

                AsteroidPresenter asteroid = instance.GetComponent<AsteroidPresenter>();
                asteroid.Init(_helper, _model.Speed);
                asteroid.Exploded += OnExploded;
                _asteroids[i] = asteroid;
            }
            
            StartCoroutine(Spawn());
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _asteroids.Length; i++)
            {
                _asteroids[i].Exploded -= OnExploded;
            }
        }

        private IEnumerator Spawn()
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

                yield return _waitDelay;
            }
        }

        public void Init(GameObject prefab, AsteroidSpawner model, Helper helper, Score score)
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