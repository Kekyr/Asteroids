namespace Model
{
    public class Score
    {
        private uint _numberOfPoints;

        public uint NumberOfPoints => _numberOfPoints;

        public void Add(uint points)
        {
            _numberOfPoints += points;
        }
    }
}