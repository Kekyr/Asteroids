using BulletBase;
using Game;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerBase
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private float _bulletSpeed;

        private Helper _helper;
        private PlayerInput _input;
        private ObjectPool _pool;

        private void Start()
        {
            _pool = GetComponent<ObjectPool>();

            _input.Player.Gun.performed += OnGunPerformed;
        }

        private void OnDestroy()
        {
            _input.Player.Gun.performed -= OnGunPerformed;
        }

        public void Init(PlayerInput input, Helper helper)
        {
            _input = input;
            _helper = helper;
            enabled = true;
        }

        private void OnGunPerformed(InputAction.CallbackContext context)
        {
            GameObject instance = _pool.Spawn(_spawnPosition.position);
            instance.transform.rotation = Quaternion.LookRotation(Vector3.forward,_spawnPosition.transform.up);

            BulletMovement bulletMovement = instance.GetComponent<BulletMovement>();
            bulletMovement.Init(_helper, _spawnPosition.transform.up, _bulletSpeed);
        }
    }
}