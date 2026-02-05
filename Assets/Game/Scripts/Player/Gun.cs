using Game;
using UnityEngine;

namespace Player
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private Bullet _prefab;

        private GunData _model;
        private Helper _helper;
        private Bullet[] _bullets;

        private int _currentIndex;

        private void Start()
        {
            _bullets = new Bullet[_model.PoolCount];
            
            for (int i = 0; i < _model.PoolCount; i++)
            {
                Bullet bullet = Instantiate(_prefab, transform);
                bullet.Init(_helper);
                _bullets[i] = bullet;
                bullet.gameObject.SetActive(false);
            }
            
            _model.Shot += OnShot;
        }

        private void OnDestroy()
        {
            _model.Shot -= OnShot;
        }

        public void Init(GunData model, Helper helper)
        {
            _model = model;
            _helper = helper;
        }
        
        private void OnShot(BulletData bulletData)
        {
            Bullet bullet = _bullets[_currentIndex];
            bullet.transform.position = _spawnPosition.position;
            bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward,_spawnPosition.transform.up);
            bullet.Init(bulletData,_spawnPosition.transform.up);
            bullet.gameObject.SetActive(true);
            _currentIndex++;
            
            if (_currentIndex >= _bullets.Length)
            {
                _currentIndex = 0;
            }
        }
    }
}