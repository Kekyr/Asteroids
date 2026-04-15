using System;
using R3;
using UnityEngine.SceneManagement;
using View;
using Zenject;

namespace Game
{
    public class SceneLoader : IInitializable, IDisposable
    {
        private GameOverView _view;
        private IDisposable _disposable;

        public SceneLoader(GameOverView view)
        {
            _view = view;
        }

        public void Initialize()
        {
            _disposable = _view.RestartButtonClicked.AsObservable().Subscribe(_ => ReloadScene());
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}