using System;
using AsteroidBase;
using PlayerBase;
using UnityEngine;
using Movement = PlayerBase.Movement;

namespace Game
{
    public class CompositeRoot : MonoBehaviour
    {
        [SerializeField] private GameObject _ship;
        [SerializeField] private Helper _helper;
        [SerializeField] private AsteroidSpawner _asteroidSpawner;
        
        private Gun _gun;
        private Movement _movement;
        private PlayerInput _input;
        
        private void Awake()
        {
            Validate();

            _movement = _ship.GetComponent<Movement>();
            _gun = _ship.GetComponentInChildren<Gun>();
            
            _input = new PlayerInput();
            _input.Enable();
            
            _movement.Init(_input, _helper);
            _gun.Init(_input);
            
            _asteroidSpawner.Init(_helper);
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void Validate()
        {
            if (_movement == null)
            {
                throw new ArgumentNullException(nameof(_movement));
            }
            
            if (_helper == null)
            {
                throw new ArgumentNullException(nameof(_helper));
            }

            if (_asteroidSpawner == null)
            {
                throw new ArgumentNullException(nameof(_asteroidSpawner));
            }
        }
    }
}