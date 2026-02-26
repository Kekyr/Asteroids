using Game;
using UnityEngine;

namespace Player
{
    public class Gun
    {
        private GunData _data;
        private Helper _helper;
        private Bullet[] _bullets;
        private Transform _spawnPosition;
        
        private GameObject _container;

        private int _currentIndex;

        public Gun(Helper helper, GunData data, Transform spawnPosition)
        {
            _helper = helper;
            _data = data;
            _spawnPosition = spawnPosition;
        }
        
        public void Start()
        {
            _bullets = new Bullet[_data.PoolCount];
            _container = new GameObject(_data.Prefab.name);
            _container.transform.parent = _spawnPosition.parent;

            for (int i = 0; i < _data.PoolCount; i++)
            {
                Bullet bullet = GameObject.Instantiate(_data.Prefab, _container.transform);
                bullet.gameObject.SetActive(false);
                bullet.Construct(_helper, _data.BulletSpeed);
                _bullets[i] = bullet;
            }
        }
        
        public void Shoot()
        {
            Bullet bullet = _bullets[_currentIndex];
            bullet.transform.position = _spawnPosition.position;
            bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, _spawnPosition.transform.up);
            bullet.SetDirection(_spawnPosition.transform.up);
            bullet.gameObject.SetActive(true);
            _currentIndex++;

            if (_currentIndex >= _bullets.Length)
            {
                _currentIndex = 0;
            }
        }
    }
}