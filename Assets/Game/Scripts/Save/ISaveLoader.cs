namespace Game.Save
{
    public interface ISaveLoader
    {
        public void Save(uint score);
        public SaveData Load();
    }
}