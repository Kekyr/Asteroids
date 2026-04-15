using R3;

namespace Game
{
    public class Score
    {
        public ReactiveProperty<uint> NumberOfPoints { get; } = new ReactiveProperty<uint>();

        public void Add(uint points)
        {
            NumberOfPoints.Value += points;
        }
    }
}