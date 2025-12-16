using System;
using AsteroidBase;
using Game.Scripts;
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
        [SerializeField] private AsteroidFragmentSpawner _asteroidFragmentSpawner;

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
            _gun.Init(_input, _helper);

            _asteroidSpawner.Init(_helper);
            _asteroidFragmentSpawner.Init(_helper,_asteroidSpawner);
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void Validate()
        {
            if (_ship == null)
            {
                throw new ArgumentNullException(nameof(_ship));
            }
            
            if (_helper == null)
            {
                throw new ArgumentNullException(nameof(_helper));
            }

            if (_asteroidSpawner == null)
            {
                throw new ArgumentNullException(nameof(_asteroidSpawner));
            }

            if (_asteroidFragmentSpawner == null)
            {
                throw new ArgumentNullException(nameof(_asteroidFragmentSpawner));
            }
        }
    }
}