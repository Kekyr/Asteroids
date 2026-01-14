namespace Model
{
    public class Score
    {
        public uint NumberOfPoints { get; private set; }

        public void Add(uint points)
        {
            NumberOfPoints += points;
        }
    }
}