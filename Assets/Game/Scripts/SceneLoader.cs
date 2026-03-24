using System;
using R3;
using UnityEngine.SceneManagement;
using View;

namespace Game
{
    public class SceneLoader : IDisposable
    {
        private IDisposable _disposable;

        public SceneLoader(GameOverView gameOverView)
        {
            _disposable = gameOverView.RestartButtonClicked.AsObservable().Subscribe(ReloadScene);
        }

        void IDisposable.Dispose()
        {
            _disposable.Dispose();
        }

        private void ReloadScene(Unit unit)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}