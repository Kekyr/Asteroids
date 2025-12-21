using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using ScoreBase;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

namespace AsteroidBase
{
    public class AsteroidSpawner : MonoBehaviour
    {
        [SerializeField] private int _poolCount;
        [SerializeField] private GameObject _prefab;

        [SerializeField] private float _delay;
        [SerializeField] private float _minPositionX;
        [SerializeField] private float _maxPositionX;
        [SerializeField] private float _positionY;

        private Queue<GameObject> _instances = new Queue<GameObject>();
        private Asteroid[] _asteroids;
        private Helper _helper;
        private Score _score;
        private WaitForSeconds _waitDelay;

        public event Action<Vector2> Exploded;

        private void OnEnable()
        {
            _waitDelay = new WaitForSeconds(_delay);
            _asteroids = new Asteroid[_poolCount];

            for (int i = 0; i < _poolCount; i++)
            {
                GameObject instance = Instantiate(_prefab, transform);
                instance.SetActive(false);

                Asteroid asteroid = instance.GetComponent<Asteroid>();
                asteroid.Init(_score);
                asteroid.Exploded += OnExploded;

                AsteroidMovement asteroidMovement = instance.GetComponent<AsteroidMovement>();
                asteroidMovement.Init(_helper);

                _asteroids[i] = asteroid;
                _instances.Enqueue(instance);
            }
        }

        private void Start()
        {
            StartCoroutine(Spawn());
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _asteroids.Length; i++)
            {
                _asteroids[i].Exploded -= OnExploded;
            }
        }

        public void Init(Helper helper, Score score)
        {
            _helper = helper;
            _score = score;
            enabled = true;
        }

        private IEnumerator Spawn()
        {
            while (isActiveAndEnabled)
            {
                Vector2 randomPosition;
                Vector2 randomDirection;

                float randomXPosition = Random.Range(_minPositionX, _maxPositionX);
                randomPosition = new Vector2(randomXPosition, _positionY);

                GameObject instance = Spawn(randomPosition);

                AsteroidMovement asteroidMovement = instance.GetComponent<AsteroidMovement>();
                randomDirection = CalculateRandomDirection();
                asteroidMovement.Init(randomDirection);

                yield return _waitDelay;
            }
        }

        public GameObject Spawn(Vector3 position)
        {
            GameObject instance = _instances.Dequeue();

            instance.transform.position = position;
            instance.gameObject.SetActive(true);

            _instances.Enqueue(instance);

            return instance;
        }

        private Vector2 CalculateRandomDirection()
        {
            float randomXDirection = Random.Range(Vector2.left.x, Vector2.right.x);
            float randomYDirection = Random.Range(Vector2.down.y, Vector2.up.y);

            return new Vector2(randomXDirection, randomYDirection);
        }

        private void OnExploded(Vector2 position)
        {
            Exploded?.Invoke(position);
        }
    }
}