using BulletBase;
using Game;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

namespace PlayerBase
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private float _bulletSpeed;
        
        private PlayerInput _input;
        private ObjectPool _pool;

        private void Awake()
        {
            _pool = GetComponent<ObjectPool>();
            
            _input.Player.Gun.performed += OnGunPerformed;
        }

        private void OnDestroy()
        {
            _input.Player.Gun.performed -= OnGunPerformed;
        }

        public void Init(PlayerInput input)
        {
            _input = input;
            enabled = true;
        }

        private void OnGunPerformed(InputAction.CallbackContext context)
        {
            GameObject instance = _pool.Spawn(_spawnPosition.position);

            BulletMovement bulletMovement = instance.GetComponent<BulletMovement>();
            bulletMovement.Init(_spawnPosition.transform.up, _bulletSpeed);
        }
    }
}