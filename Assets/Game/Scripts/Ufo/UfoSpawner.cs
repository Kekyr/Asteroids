using System.Collections;
using System.Collections.Generic;
using ScoreBase;
using UfoBase;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts
{
    public class UfoSpawner : MonoBehaviour
    {
        [SerializeField] private int _poolCount;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private float _speed;

        [SerializeField] private float _delay;
        [SerializeField] private float _minPositionY;
        [SerializeField] private float _maxPositionY;
        [SerializeField] private float _positionX;

        private Queue<GameObject> _instances = new Queue<GameObject>();
        private Helper _helper;
        private Score _score;
        private WaitForSeconds _waitDelay;
        private Transform _player;

        private void OnEnable()
        {
            _waitDelay = new WaitForSeconds(_delay);

            for (int i = 0; i < _poolCount; i++)
            {
                GameObject instance = Instantiate(_prefab, transform);
                instance.SetActive(false);

                Ufo ufo = instance.GetComponent<Ufo>();
                ufo.Init(_score);

                UfoMovement ufoMovement = instance.GetComponent<UfoMovement>();
                ufoMovement.Init(_player, _helper, _speed);

                _instances.Enqueue(instance);
            }
        }

        private void Start()
        {
            StartCoroutine(Spawn());
        }

        public void Init(Helper helper, Score score, Transform player)
        {
            _helper = helper;
            _score = score;
            _player = player;
            enabled = true;
        }

        private IEnumerator Spawn()
        {
            while (isActiveAndEnabled)
            {
                Vector2 randomPosition;

                float randomYPosition = Random.Range(_minPositionY, _maxPositionY);
                randomPosition = new Vector2(_positionX, randomYPosition);
                Spawn(randomPosition);

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
    }
}