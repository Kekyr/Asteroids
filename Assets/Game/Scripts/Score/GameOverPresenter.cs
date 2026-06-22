using System;
using R3;
using Zenject;

namespace Game
{
    public class GameOverPresenter : IInitializable, IDisposable
    {
        private WinLoseController _winLoseController;
        private GameOverView _view;
        private IDisposable _disposable;

        public GameOverPresenter(WinLoseController winLoseController, GameOverView view)
        {
            _winLoseController = winLoseController;
            _view = view;
        }

        public void Initialize()
        {
            _disposable = _winLoseController.IsGameOver.Subscribe(x => OnGameOver(x));
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void OnGameOver(bool isGameOver)
        {
            _view.gameObject.SetActive(isGameOver);
        }
    }
}