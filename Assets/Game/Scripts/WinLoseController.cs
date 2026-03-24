using System;
using Player;
using R3;
using View;

namespace Game
{
    public class WinLoseController : IDisposable
    {
        private IDisposable _disposable;

        public WinLoseController(Ship ship, GameOverView gameOverView)
        {
            _disposable = ship.IsDestroyed.Subscribe(x => gameOverView.gameObject.SetActive(x));
        }

        void IDisposable.Dispose()
        {
            _disposable.Dispose();
        }
    }
}