using System.Collections;
using UnityEngine;
using Game;
using Model;

namespace Presenter
{
    public class UfoSpawnerPresenter : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;

        private UfoSpawner _model;

        private Helper _helper;
        private Score _score;
        private WaitForSeconds _waitDelay;
        private Transform _player;

        private UfoPresenter[] _ufos;
        private int _currentIndex;

        private void Start()
        {
            _waitDelay = new WaitForSeconds(_model.Delay);
            _ufos = new UfoPresenter[_model.PoolCount];

            for (int i = 0; i < _model.PoolCount; i++)
            {
                GameObject ufo = Instantiate(_prefab, transform);
                ufo.SetActive(false);

                UfoPresenter ufoPresenter = ufo.GetComponent<UfoPresenter>();
                ufoPresenter.Init(_player, _helper);
                ufoPresenter.Exploded += OnExploded;
                _ufos[i] = ufoPresenter;
            }
            
            StartCoroutine(Spawn());
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _model.PoolCount; i++)
            {
                _ufos[i].Exploded -= OnExploded;
            }
        }

        private IEnumerator Spawn()
        {
            while (isActiveAndEnabled)
            {
                Ufo ufo = _model.Spawn();
                UfoPresenter ufoPresenter = _ufos[_currentIndex];
                ufoPresenter.Init(ufo);
                ufoPresenter.gameObject.SetActive(true);
                _currentIndex++;

                if (_currentIndex >= _ufos.Length)
                {
                    _currentIndex = 0;
                }

                yield return _waitDelay;
            }
        }

        public void Init(UfoSpawner model, Helper helper, Transform player, Score score)
        {
            _model = model;
            _helper = helper;
            _player = player;
            _score = score;
            enabled = true;
        }

        private void OnExploded()
        {
            _score.Add(_model.Points);
        }
    }
}