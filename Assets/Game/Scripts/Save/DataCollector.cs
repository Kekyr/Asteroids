using Game;
using Zenject;

namespace Save
{
    public class DataCollector : IInitializable
    {
        private Score _score;
        private ISaveLoader _saveLoader;
        private SaveData _currentSaveData;

        public DataCollector(Score score, ISaveLoader saveLoader)
        {
            _score = score;
            _saveLoader = saveLoader;
        }

        public void Initialize()
        {
            Load();
        }

        public void Save()
        {
            _currentSaveData.HighScore = _score.CurrentScore.Value;
            _saveLoader.Save(_currentSaveData);
        }

        public void Load()
        {
            SaveData saveData = _saveLoader.Load();

            if (saveData == null)
            {
                return;
            }

            _score.Initialize(saveData.HighScore);
        }
    }
}