using System;
using Game;
using R3;

namespace ViewModel
{
    public class GameOverViewModel : IDisposable
    {
        public readonly ReactiveProperty<string> Score;

        private CompositeDisposable _disposables;

        public GameOverViewModel(Score score)
        {
            Score = new ReactiveProperty<string>();
            _disposables = new CompositeDisposable();
            score.NumberOfPoints.Subscribe(x => Score.Value = $"Score: {x}").AddTo(_disposables);
        }

        void IDisposable.Dispose()
        {
            _disposables.Dispose();
        }
    }
}