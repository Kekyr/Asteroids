using Game.Save;
using R3;
using Zenject;

namespace Game
{
    public class Score : IInitializable
    {
        private ISaveLoader _saveLoader;

        public ReactiveProperty<uint> HighScore { get; } = new ReactiveProperty<uint>();
        public ReactiveProperty<uint> CurrentScore { get; } = new ReactiveProperty<uint>();

        public Score(ISaveLoader saveLoader)
        {
            _saveLoader = saveLoader;
        }

        public void Initialize()
        {
            CurrentScore.Value = 0;
            
            SaveData saveData = _saveLoader.Load();

            if (saveData == null)
            {
                HighScore.Value = 0;
                return;
            }

            HighScore.Value = saveData.HighScore;
        }

        public void Add(uint points)
        {
            CurrentScore.Value += points;
        }

        public void OnShipDestroyed()
        {
            if (CurrentScore.Value > HighScore.Value)
            {
                _saveLoader.Save(CurrentScore.Value);
            }
        }
    }
}