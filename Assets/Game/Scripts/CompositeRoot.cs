using System;
using AsteroidBase;
using Game.Scripts;
using PlayerBase;
using ScoreBase;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using Movement = PlayerBase.Movement;

namespace Game
{
    public class CompositeRoot : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private AsteroidSpawner _asteroidSpawner;
        [SerializeField] private AsteroidFragmentSpawner _asteroidFragmentSpawner;
        [SerializeField] private UfoSpawner _ufoSpawner;

        [SerializeField] private Helper _helper;
        [SerializeField] private Score _score;

        [SerializeField] private LaserGunView _laserGunView;
        [SerializeField] private ShipView _shipView;
        [SerializeField] private GameOverPopup _gameOverPopup;

        private Gun _gun;
        private LaserGun _laserGun;
        private Ship _ship;
        private Movement _movement;
        private PlayerInput _input;

        private void Awake()
        {
            Validate();

            _ship = _player.GetComponent<Ship>();
            _movement = _player.GetComponent<Movement>();
            _gun = _player.GetComponentInChildren<Gun>();
            _laserGun = _player.GetComponentInChildren<LaserGun>();

            _laserGunView.Init(_laserGun);
            _shipView.Init(_movement);
            _gameOverPopup.Init(_score, _ship);

            _input = new PlayerInput();
            _input.Enable();

            _movement.Init(_input, _helper);
            _gun.Init(_input, _helper);
            _laserGun.Init(_input);

            _asteroidSpawner.Init(_helper, _score);
            _asteroidFragmentSpawner.Init(_helper, _score, _asteroidSpawner);
            _ufoSpawner.Init(_helper, _score, _player.transform);
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void Validate()
        {
            if (_player == null)
            {
                throw new ArgumentNullException(nameof(_player));
            }

            if (_asteroidSpawner == null)
            {
                throw new ArgumentNullException(nameof(_asteroidSpawner));
            }

            if (_asteroidFragmentSpawner == null)
            {
                throw new ArgumentNullException(nameof(_asteroidFragmentSpawner));
            }

            if (_ufoSpawner == null)
            {
                throw new ArgumentNullException(nameof(_ufoSpawner));
            }

            if (_helper == null)
            {
                throw new ArgumentNullException(nameof(_helper));
            }

            if (_score == null)
            {
                throw new ArgumentNullException(nameof(_score));
            }

            if (_laserGunView == null)
            {
                throw new ArgumentNullException(nameof(_laserGunView));
            }

            if (_shipView == null)
            {
                throw new ArgumentNullException(nameof(_shipView));
            }

            if (_gameOverPopup == null)
            {
                throw new ArgumentNullException(nameof(_gameOverPopup));
            }
        }
    }
}