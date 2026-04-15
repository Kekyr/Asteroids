using System;
using Game;
using R3;
using Zenject;

namespace ViewModel
{
    public class GameOverViewModel : IInitializable, IDisposable
    {
        public readonly ReactiveProperty<string> Score;

        private Score _model;
        private CompositeDisposable _disposables;

        public GameOverViewModel(Score model)
        {
            _model = model;
            Score = new ReactiveProperty<string>();
            _disposables = new CompositeDisposable();
        }

        public void Initialize()
        {
            _model.NumberOfPoints.Subscribe(x => Score.Value = $"Score: {x}").AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}