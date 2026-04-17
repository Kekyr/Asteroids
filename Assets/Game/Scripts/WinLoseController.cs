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
        private Score _score;
        private GameOverView _gameOverView;
        private IDisposable _disposable;

        public WinLoseController(Ship ship, GameOverView gameOverView, Score score)
        {
            _ship = ship;
            _gameOverView = gameOverView;
            _score = score;
        }

        public void Initialize()
        {
            _disposable = _ship.IsDestroyed.Subscribe(x => OnShipDestroyed(x));
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void OnShipDestroyed(bool isDestroyed)
        {
            if (isDestroyed == false)
            {
                return;
            }

            _gameOverView.gameObject.SetActive(isDestroyed);
            _score.OnShipDestroyed();
        }
    }
}