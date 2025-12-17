using System.Collections.Generic;
using AsteroidBase;
using ScoreBase;
using UnityEngine;

namespace Game.Scripts
{
    public class AsteroidFragmentSpawner : MonoBehaviour
    {
        [SerializeField] private int _poolCount;
        [SerializeField] private int _explodeCount;
        [SerializeField] private GameObject _prefab;

        [SerializeField] private float _positionXOffset;
        [SerializeField] private float _positionYOffset;

        private Queue<GameObject> _instances = new Queue<GameObject>();
        private Helper _helper;
        private Score _score;
        private AsteroidSpawner _asteroidSpawner;

        private void Start()
        {
            for (int i = 0; i < _poolCount; i++)
            {
                GameObject instance = Instantiate(_prefab, transform);
                instance.SetActive(false);

                Asteroid asteroid = instance.GetComponent<Asteroid>();
                asteroid.Init(_score);

                AsteroidMovement asteroidMovement = instance.GetComponent<AsteroidMovement>();
                asteroidMovement.Init(_helper);

                _instances.Enqueue(instance);
            }

            _asteroidSpawner.Exploded += OnExploded;
        }

        private void OnDestroy()
        {
            _asteroidSpawner.Exploded -= OnExploded;
        }

        public void Init(Helper helper, Score score, AsteroidSpawner asteroidSpawner)
        {
            _helper = helper;
            _score = score;
            _asteroidSpawner = asteroidSpawner;
            enabled = true;
        }

        private void OnExploded(Vector2 position)
        {
            for (int i = 0; i < _explodeCount; i++)
            {
                Vector2 randomPosition;
                Vector2 randomDirection;

                float randomXPosition = Random.Range(position.x - _positionXOffset, position.x + _positionXOffset);
                float randomYPosition = Random.Range(position.y - _positionYOffset, position.y + _positionYOffset);

                randomPosition = new Vector2(randomXPosition, randomYPosition);

                GameObject instance = Spawn(randomPosition);

                AsteroidMovement asteroidMovement = instance.GetComponent<AsteroidMovement>();
                randomDirection = CalculateRandomDirection();

                asteroidMovement.Init(randomDirection);
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
    }
}