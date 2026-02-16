using UnityEngine;
using Game;
using Player;
using R3;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ufo : MonoBehaviour
    {
        private UfoData _model;

        private Rigidbody2D _rigidbody;
        private Transform _target;
        private Helper _helper;
        private SpriteRenderer _view;

        private bool _isOnScreen;

        public ReactiveProperty<bool> IsExploded { get; } = new ReactiveProperty<bool>();

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _view = GetComponentInChildren<SpriteRenderer>();

            _rigidbody.centerOfMass = _view.transform.localPosition;
        }

        private void FixedUpdate()
        {
            if (_target.gameObject.activeSelf == true)
            {
                Vector2 direction = (_target.position - transform.position).normalized;
                _rigidbody.rotation = Quaternion.LookRotation(Vector3.forward, direction).eulerAngles.z;
                _rigidbody.linearVelocity = direction * _model.Speed;

                if (_isOnScreen == false)
                {
                    _isOnScreen = _helper.IsOnScreen(transform.position);
                    return;
                }

                transform.position = _helper.ClampPosition(transform.position);
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            IsExploded.Value = true;
            gameObject.SetActive(false);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<Bullet>(out Bullet bullet) == false)
            {
                return;
            }
            
            IsExploded.Value = true;
            gameObject.SetActive(false);
        }

        public void Init(Transform target, Helper helper)
        {
            _target = target;
            _helper = helper;
        }

        public void Init(UfoData model)
        {
            _model = model;
            transform.position = _model.Transform.Position.Value;
        }
    }
}