using System;
using Game;
using R3;
using Zenject;

namespace Game
{
    public class GameOverViewModel : IInitializable, IDisposable
    {
        public readonly ReactiveProperty<string> HighScore;
        public readonly ReactiveProperty<string> Score;

        private Score _model;
        private CompositeDisposable _disposables;

        public GameOverViewModel(Score model)
        {
            _model = model;
            _disposables = new CompositeDisposable();
            HighScore = new ReactiveProperty<string>();
            Score = new ReactiveProperty<string>();
        }

        public void Initialize()
        {
            _model.CurrentScore.Subscribe(x => Score.Value = $"Score: {x}").AddTo(_disposables);
            _model.HighScore.Subscribe(x => HighScore.Value = $"High Score: {x}").AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}