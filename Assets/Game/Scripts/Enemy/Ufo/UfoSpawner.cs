using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Game;
using R3;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class UfoSpawner
    {
        private UfoSpawnerData _data;
        private GameObject _container;

        private Helper _helper;
        private Score _score;
        private Transform _player;

        private Ufo[] _ufos;
        private int _currentIndex;

        private bool _isActive = true;

        public UfoSpawner(UfoSpawnerData data, Helper helper, Transform player, Score score)
        {
            _data = data;
            _helper = helper;
            _player = player;
            _score = score;
        }

        public void Start()
        {
            _ufos = new Ufo[_data.PoolCount];
            _container = new GameObject(_data.Prefab.name);

            for (int i = 0; i < _data.PoolCount; i++)
            {
                Ufo ufo = GameObject.Instantiate(_data.Prefab, _container.transform);
                ufo.gameObject.SetActive(false);

                ufo.Construct(_player, _helper, _data.Speed);
                ufo.IsExploded.Skip(1).Subscribe(_ => _score.Add(_data.Points)).AddTo(ufo);
                _ufos[i] = ufo;
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
                Ufo ufo = _ufos[_currentIndex];
                ufo.transform.position = CalculateRandomPosition();
                ufo.gameObject.SetActive(true);
                _currentIndex++;

                if (_currentIndex >= _ufos.Length)
                {
                    _currentIndex = 0;
                }

                await UniTask.Delay(TimeSpan.FromSeconds(_data.Delay), ignoreTimeScale: false);
            }
        }

        private Vector2 CalculateRandomPosition()
        {
            float randomYPosition = Random.Range(_data.MinPositionY, _data.MaxPositionY);
            Vector2 randomPosition = new Vector2(_data.PositionX, randomYPosition);
            return randomPosition;
        }
    }
}