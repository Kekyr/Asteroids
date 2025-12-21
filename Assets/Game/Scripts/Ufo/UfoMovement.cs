using Game;
using UnityEngine;

namespace UfoBase
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class UfoMovement : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private Transform _target;
        private Helper _helper;
        
        private float _speed;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (_target.gameObject.activeSelf == true)
            {
                Vector2 direction = (_target.position - transform.position).normalized;
                transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
                _rigidbody.linearVelocity = direction * _speed;
                transform.position = _helper.ClampPosition(transform.position);
            }
        }

        public void Init(Transform target, Helper helper, float speed)
        {
            _target = target;
            _helper = helper;
            _speed = speed;
        }
    }
}
