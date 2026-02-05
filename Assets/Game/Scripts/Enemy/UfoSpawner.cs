using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Game;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class UfoSpawner
    {
        private Ufo _prefab;
        private GameObject _container;

        private Helper _helper;
        private Score _score;
        private Transform _player;

        private Queue<UfoData> _queue = new Queue<UfoData>();
        private Ufo[] _ufos;
        private int _currentIndex;

        private bool _isActive = true;
        private int _poolCount;
        private float _speed;
        private float _delay;
        private float _minPositionY;
        private float _maxPositionY;
        private float _positionX;
        private uint _points;

        public UfoSpawner(Ufo prefab, Helper helper, Transform player, Score score)
        {
            _prefab = prefab;
            _helper = helper;
            _player = player;
            _score = score;

            SetDefaultValues();

            for (int i = 0; i < _poolCount; i++)
            {
                UfoData ufoData = new UfoData(_speed);
                _queue.Enqueue(ufoData);
            }
        }

        private void SetDefaultValues()
        {
            _poolCount = 10;
            _speed = 1;
            _delay = 6;
            _minPositionY = 0;
            _maxPositionY = 12;
            _positionX = 11;
            _points = 150;
        }

        public void Start()
        {
            _ufos = new Ufo[_poolCount];
            _container = new GameObject(_prefab.name);

            for (int i = 0; i < _poolCount; i++)
            {
                Ufo ufo = GameObject.Instantiate(_prefab, _container.transform);
                ufo.gameObject.SetActive(false);
                
                ufo.Init(_player, _helper);
                ufo.Exploded += OnExploded;
                _ufos[i] = ufo;
            }

            Spawn().Forget();
        }

        public void OnDestroy()
        {
            _isActive = false;

            for (int i = 0; i < _poolCount; i++)
            {
                _ufos[i].Exploded -= OnExploded;
            }
        }

        private async UniTask Spawn()
        {
            while (_isActive)
            {
                UfoData ufoData = GetInstance();
                Ufo ufo = _ufos[_currentIndex];
                ufo.Init(ufoData);
                ufo.gameObject.SetActive(true);
                _currentIndex++;

                if (_currentIndex >= _ufos.Length)
                {
                    _currentIndex = 0;
                }

                await UniTask.Delay(TimeSpan.FromSeconds(_delay), ignoreTimeScale: false);
            }
        }

        private UfoData GetInstance()
        {
            UfoData ufoData = _queue.Dequeue();
            ufoData.Transform.ChangePosition(CalculateRandomPosition());
            _queue.Enqueue(ufoData);
            return ufoData;
        }

        private Vector2 CalculateRandomPosition()
        {
            float randomYPosition = Random.Range(_minPositionY, _maxPositionY);
            Vector2 randomPosition = new Vector2(_positionX, randomYPosition);
            return randomPosition;
        }

        private void OnExploded()
        {
            _score.Add(_points);
        }
    }
}