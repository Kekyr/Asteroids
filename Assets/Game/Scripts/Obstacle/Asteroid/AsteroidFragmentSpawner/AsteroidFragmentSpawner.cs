using UnityEngine;
using Game;
using R3;

namespace Obstacle
{
    public class AsteroidFragmentSpawner : MonoBehaviour
    {
        private Asteroid _prefab;
        private GameObject _container;

        private AsteroidFragmentSpawnerData _model;
        private Helper _helper;
        private Score _score;
        private AsteroidSpawner _asteroidSpawner;
        private Asteroid[] _asteroidFragments;

        private int _currentIndex;

        private void Start()
        {
            _asteroidFragments = new Asteroid[_model.PoolCount];
            _container = new GameObject(_prefab.name);

            for (int i = 0; i < _model.PoolCount; i++)
            {
                Asteroid asteroid = Instantiate(_prefab, _container.transform);
                asteroid.gameObject.SetActive(false);
                
                asteroid.Exploded.Subscribe(OnFragmentExploded).AddTo(asteroid);
                _asteroidFragments[i] = asteroid;
                asteroid.Init(_helper, _model.Speed);
            }

            _asteroidSpawner.Exploded.Subscribe(OnExploded).AddTo(this);
        }

        public void Init(Asteroid prefab, AsteroidFragmentSpawnerData model, Helper helper,
            AsteroidSpawner asteroidSpawner, Score score)
        {
            _prefab = prefab;
            _model = model;
            _helper = helper;
            _asteroidSpawner = asteroidSpawner;
            _score = score;
        }

        private void OnExploded(Vector2 position)
        {
            for (int i = 0; i < _model.ExplodeCount; i++)
            {
                AsteroidData asteroidData = _model.Spawn(position);
                Asteroid asteroid = _asteroidFragments[_currentIndex];
                asteroid.Init(asteroidData);
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
            _score.Add(_model.Points);
        }
    }
}