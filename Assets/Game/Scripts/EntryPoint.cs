using Enemy;
using Player;
using UnityEngine;

namespace Game
{
    public class EntryPoint : MonoBehaviour
    {
        private PlayerInputRouter _playerInputRouter;
        private LaserGunData _laserGunData;
        private UfoSpawner _ufoSpawner;
        
        private void Start()
        {
            _playerInputRouter.Start();
            _ufoSpawner.Start();
        }

        private void OnDestroy()
        {
            _playerInputRouter.OnDestroy();
            _ufoSpawner.OnDestroy();
        }

        private void Update()
        {
            _laserGunData.Update(Time.deltaTime);
        }

        public void Init(PlayerInputRouter playerInputRouter, LaserGunData laserGunData, UfoSpawner ufoSpawner)
        {
            _playerInputRouter = playerInputRouter;
            _laserGunData = laserGunData;
            _ufoSpawner = ufoSpawner;
        }
    }
}