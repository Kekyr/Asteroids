using UnityEngine;
using Game;
using Model;

namespace Presenter
{
    public class GunPresenter : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private GameObject _prefab;

        private Gun _model;
        private Helper _helper;
        private BulletPresenter[] _bullets;

        private int _currentIndex;

        private void Start()
        {
            _bullets = new BulletPresenter[_model.PoolCount];
            
            for (int i = 0; i < _model.PoolCount; i++)
            {
                GameObject instance = Instantiate(_prefab, transform);
                BulletPresenter bulletPresenter = instance.GetComponent<BulletPresenter>();
                bulletPresenter.Init(_helper);
                _bullets[i] = bulletPresenter;
                instance.SetActive(false);
            }
            
            _model.Shot += OnShot;
        }

        private void OnDestroy()
        {
            _model.Shot -= OnShot;
        }

        public void Init(Gun model, Helper helper)
        {
            _model = model;
            _helper = helper;
            enabled = true;
        }
        
        private void OnShot(Bullet bullet)
        {
            BulletPresenter bulletPresenter = _bullets[_currentIndex];
            bulletPresenter.transform.position = _spawnPosition.position;
            bulletPresenter.transform.rotation = Quaternion.LookRotation(Vector3.forward,_spawnPosition.transform.up);
            bulletPresenter.Init(bullet,_spawnPosition.transform.up);
            bulletPresenter.gameObject.SetActive(true);
            _currentIndex++;
            
            if (_currentIndex >= _bullets.Length)
            {
                _currentIndex = 0;
            }
        }
    }
}