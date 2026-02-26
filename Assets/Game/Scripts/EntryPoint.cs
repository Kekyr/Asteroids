using Enemy;
using Obstacle;
using Player;
using UnityEngine;

namespace Game
{
    public class EntryPoint : MonoBehaviour
    {
        private PlayerInputRouter _playerInputRouter;
        private LaserGun _laserGun;
        
        private UfoSpawner _ufoSpawner;
        private AsteroidFragmentSpawner _asteroidFragmentSpawner;
        private AsteroidSpawner _asteroidSpawner;
        private Gun _gun;
        
        private void Start()
        {
            _playerInputRouter.Start();
            _ufoSpawner.Start();
            _asteroidFragmentSpawner.Start();
            _asteroidSpawner.Start();
            _gun.Start();
        }

        private void OnDestroy()
        {
            _playerInputRouter.OnDestroy();
            _ufoSpawner.OnDestroy();
            _asteroidFragmentSpawner.OnDestroy();
            _asteroidSpawner.OnDestroy();
        }

        private void Update()
        {
            _laserGun.Update(Time.deltaTime);
        }

        public void Construct(PlayerInputRouter playerInputRouter, LaserGun laserGun, UfoSpawner ufoSpawner, AsteroidFragmentSpawner asteroidFragmentSpawner,
            AsteroidSpawner asteroidSpawner, Gun gun)
        {
            _playerInputRouter = playerInputRouter;
            _laserGun = laserGun;
            _ufoSpawner = ufoSpawner;
            _asteroidFragmentSpawner = asteroidFragmentSpawner;
            _asteroidSpawner = asteroidSpawner;
            _gun = gun;
        }
    }
}