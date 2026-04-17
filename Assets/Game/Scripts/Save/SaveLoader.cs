using UnityEngine;

namespace Game.Save
{
    public class SaveLoader : ISaveLoader
    {
        private string _key = "save";

        public void Save(uint score)
        {
            SaveData saveData = new SaveData(score);
            string json = JsonUtility.ToJson(saveData);
            PlayerPrefs.SetString(_key, json);
        }

        public SaveData Load()
        {
            string json = PlayerPrefs.GetString(_key);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            return saveData;
        }
    }
}