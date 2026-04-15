using System;
using Player;
using R3;
using View;
using Zenject;

namespace Game
{
    public class WinLoseController : IInitializable, IDisposable
    {
        private Ship _ship;
        private GameOverView _gameOverView;
        private IDisposable _disposable;

        public WinLoseController(Ship ship, GameOverView gameOverView)
        {
            _ship = ship;
            _gameOverView = gameOverView;
        }

        public void Initialize()
        {
            _disposable = _ship.IsDestroyed.Subscribe(x => _gameOverView.gameObject.SetActive(x));
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}