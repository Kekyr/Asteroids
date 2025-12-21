using System;
using Model;
using Presenter;
using UnityEngine;

namespace Game
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private ShipPresenter _shipPresenter;

        [SerializeField] private Helper _helper;
        

        private Ship _model;
        private PlayerInputRouter _playerInputRouter;

        private void Awake()
        {
            Validate();

            _model = new Ship();
            _playerInputRouter = new PlayerInputRouter(_model);
            
            _shipPresenter.Init(_model, _helper);
        }

        private void OnEnable()
        {
            _playerInputRouter.OnEnable();
        }

        private void OnDisable()
        {
            _playerInputRouter.OnDisable();
        }

        private void Validate()
        {
            if (_shipPresenter == null)
            {
                throw new ArgumentNullException(nameof(_shipPresenter));
            }

            if (_helper == null)
            {
                throw new ArgumentNullException(nameof(_helper));
            }
        }
    }
}