using Game;
using R3;
using UnityEngine;

namespace Player
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private Bullet _prefab;
        [SerializeField] private int _poolCount  = 15;
        [SerializeField] private float _bulletSpeed  = 8;
        
        private Helper _helper;
        private Bullet[] _bullets;

        private int _currentIndex;

        private void Start()
        {
            _bullets = new Bullet[_poolCount];
            
            for (int i = 0; i < _poolCount; i++)
            {
                Bullet bullet = Instantiate(_prefab, transform);
                bullet.gameObject.SetActive(false);
                bullet.Init(_helper);
                _bullets[i] = bullet;
            }
        }

        public void Init(Helper helper)
        {
            _helper = helper;
        }
        
        public void Shoot()
        {
            Bullet bullet = _bullets[_currentIndex];
            bullet.transform.position = _spawnPosition.position;
            bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward,_spawnPosition.transform.up);
            bullet.Init(_bulletSpeed,_spawnPosition.transform.up);
            bullet.gameObject.SetActive(true);
            _currentIndex++;
            
            if (_currentIndex >= _bullets.Length)
            {
                _currentIndex = 0;
            }
        }
    }
}