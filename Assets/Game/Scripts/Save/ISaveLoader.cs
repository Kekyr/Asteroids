namespace Save
{
    public interface ISaveLoader
    {
        public void Save(SaveData saveData);
        public SaveData Load();
    }
}