using Zenject;

namespace Game.Save
{
    public class DataCollector : IInitializable
    {
        private Score _score;
        private ISaveLoader _saveLoader;

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
            SaveData saveData = new SaveData(_score.CurrentScore.Value);
            _saveLoader.Save(saveData);
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