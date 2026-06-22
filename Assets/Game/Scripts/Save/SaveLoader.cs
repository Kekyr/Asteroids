using UnityEngine;

namespace Save
{
    public class SaveLoader : ISaveLoader
    {
        private string _key = "save";

        public void Save(SaveData saveData)
        {
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