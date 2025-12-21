using Game;
using UnityEngine;

namespace BulletBase
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BulletMovement : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private Helper _helper;
        
        private Vector2 _direction;
        private float _speed;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody.linearVelocity = _direction * _speed;

            if (_helper.CheckPosition(transform.position) == false)
            {
                gameObject.SetActive(false);
            }
        }

        public void Init(Helper helper, Vector2 direction, float speed)
        {
            _helper = helper;
            _direction = direction;
            _speed = speed;
            enabled = true;
        }
    }
}