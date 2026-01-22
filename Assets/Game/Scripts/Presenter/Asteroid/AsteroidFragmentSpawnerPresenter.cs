using UnityEngine;
using Game;
using Model;

namespace Presenter
{
    public class AsteroidFragmentSpawnerPresenter : MonoBehaviour
    {
        private AsteroidPresenter _prefab;
        private GameObject _container;

        private AsteroidFragmentSpawner _model;
        private Helper _helper;
        private Score _score;
        private AsteroidSpawnerPresenter _asteroidSpawnerPresenter;
        private AsteroidPresenter[] _asteroidFragments;

        private int _currentIndex;

        private void Start()
        {
            _asteroidFragments = new AsteroidPresenter[_model.PoolCount];
            _container = new GameObject(_prefab.name);

            for (int i = 0; i < _model.PoolCount; i++)
            {
                AsteroidPresenter asteroid = Instantiate(_prefab, _container.transform);
                asteroid.gameObject.SetActive(false);
                
                asteroid.Exploded += OnFragmentExploded;
                _asteroidFragments[i] = asteroid;
                asteroid.Init(_helper, _model.Speed);
            }

            _asteroidSpawnerPresenter.Exploded += OnExploded;
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _model.PoolCount; i++)
            {
                _asteroidFragments[i].Exploded -= OnFragmentExploded;
            }

            _asteroidSpawnerPresenter.Exploded -= OnExploded;
        }

        public void Init(AsteroidPresenter prefab, AsteroidFragmentSpawner model, Helper helper,
            AsteroidSpawnerPresenter asteroidSpawnerPresenter, Score score)
        {
            _prefab = prefab;
            _model = model;
            _helper = helper;
            _asteroidSpawnerPresenter = asteroidSpawnerPresenter;
            _score = score;
            enabled = true;
        }

        private void OnExploded(Vector2 position)
        {
            for (int i = 0; i < _model.ExplodeCount; i++)
            {
                Asteroid asteroid = _model.Spawn(position);
                AsteroidPresenter asteroidPresenter = _asteroidFragments[_currentIndex];
                asteroidPresenter.Init(asteroid);
                asteroidPresenter.gameObject.SetActive(true);
                _currentIndex++;

                if (_currentIndex >= _asteroidFragments.Length)
                {
                    _currentIndex = 0;
                }
            }
        }

        private void OnFragmentExploded(Vector2 position)
        {
            _score.Add(_model.Points);
        }
    }
}