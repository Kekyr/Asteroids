using Game;
using Model;
using UnityEngine;

namespace Presenter
{
    public class AsteroidPresenter : MonoBehaviour
    {
        private Asteroid _model;

        private Rigidbody2D _rigidbody;
        private Helper _helper;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        
        private void FixedUpdate()
        {
            _rigidbody.linearVelocity = _model.MoveDirection * _model.Speed;
            transform.position = _helper.ClampPosition(transform.position);
        }

        public void Init(Asteroid model, Helper helper)
        {
            _model = model;
            _helper = helper;
            enabled = true;
        }
    }
}
